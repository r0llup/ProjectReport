using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ClassLibraryReport.Parsers;
using ClassLibraryReport.Renderers;
using ClassLibraryReport.Utils;
using ClassLibraryReport.View;
using DataExample;

namespace WpfApplicationReport
{
    public partial class MainWindow
    {
        public MainWindow() : this(default(DataSet))
        {
        }

        public MainWindow(DataSet dataSet) : this(dataSet, null)
        {
        }

        public MainWindow(DataSet dataSet, String reportName) :
            this(dataSet, reportName, true)
        {
        }

        public MainWindow(DataSet dataSet, String reportName, Boolean showLineHeader)
        {
            InitializeComponent();
            if (dataSet == null)
                InitDataSet();
            else
                DataSet = dataSet;
            ObjectsList = null;
            Report = null;
            ReportName = reportName;
            ShowLineHeader = showLineHeader;
            DataTableList = new List<DataTable>();
        }

        public MainWindow(List<List<Object>> objectsList) :
            this(objectsList, null)
        {
        }

        public MainWindow(List<List<Object>> objectsList, String reportName) :
            this(objectsList, reportName, true)
        {
        }

        public MainWindow(List<List<Object>> objectsList, String reportName, Boolean showLineHeader)
        {
            InitializeComponent();
            DataSet = null;
            ObjectsList = objectsList;
            Report = null;
            ReportName = reportName;
            ShowLineHeader = showLineHeader;
            DataTableList = new List<DataTable>();
        }

        public DataSet DataSet { get; private set; }
        public List<List<Object>> ObjectsList { get; private set; }
        public Report Report { get; private set; }
        public String ReportName { get; private set; }
        public Boolean ShowLineHeader { get; private set; }
        public List<DataTable> DataTableList { get; private set; }

        private void InitDataSet()
        {
            var dsPrest = new DSPrestation();
            for (int index = 0; index < 1000; index++)
            {
                var rn = dsPrest.Tables[0].NewRow() as DSPrestation.TableRow;
                if (rn == null) continue;
                rn.prest_pk = new Random().Next(99999);
                rn.prest_date = DateTime.Now.AddDays(index + 1);
                rn.prest_description = "blabla" + index;
                rn.prest_nb_unite = index + 1;
                rn.prest_taux = Convert.ToDecimal((index + 1)*1.13);
                rn.prest_user_fk = (index%2) == 0 ? 10 : 20;
                rn.user_nom = (index%2) == 0 ? "Alain" : "Bernard";
                rn.user_initiales = (index%2) == 0 ? "AF" : "BD";
                dsPrest.Tables[0].Rows.Add(rn);
            }
            DataSet = dsPrest;
        }

        private void InitReportDataTreeView()
        {
            if (DataSet == null) return;
            ReportDataTreeView.Items.Clear();
            foreach (DataTable dataTable in DataSet.Tables)
            {
                var tableName = new TreeViewItem {Header = dataTable.TableName};
                foreach (TreeViewItem tableColumn in from DataColumn dataColumn in dataTable.Columns
                                                     select
                                                         new TreeViewItem
                                                             {
                                                                 AllowDrop = true,
                                                                 Header = dataColumn.ColumnName
                                                             })
                {
                    tableColumn.MouseMove += treeViewItem_MouseMove;
                    tableName.Items.Add(tableColumn);
                }
                ReportDataTreeView.Items.Add(tableName);
            }
        }

        private void ResizeReportBody()
        {
            ReportBodyContentControl.Width = ReportCanvas.ActualWidth - 100;
            ReportBodyContentControl.Height = ReportCanvas.ActualHeight - 50;
        }

        private void Window_Loaded(Object sender, RoutedEventArgs e)
        {
            if (DataSet != null)
            {
                SimpleXmlUpdater.Update(DataSet,
                                        String.Format("Xml{0}Reports.xml", Path.DirectorySeparatorChar),
                                        ReportName ?? "Default");
                Report = DsConverter.Convert(DataSet);
            }
            else if (ObjectsList != null && ObjectsList.Count > 0)
            {
                SimpleXmlUpdater.Update(ObjectsList[0],
                                        String.Format("Xml{0}Reports.xml", Path.DirectorySeparatorChar),
                                        ReportName ?? "Default", "objectsList");
                Report = ObjConverter.Convert(ObjectsList);
            }
            new SimpleXmlParser(String.Format("Xml{0}Reports.xml", Path.DirectorySeparatorChar),
                                String.Format("Schemas{0}Reports.xsd", Path.DirectorySeparatorChar), Report).Parse();
            new HtmlRenderer(Report, "Css/Reports.css", "Css/PrintableReports.css").Render();
            Process.Start(String.Format("{0}{1}{2}.html", Directory.GetCurrentDirectory(), Path.DirectorySeparatorChar,
                                        Report.Name));
            InitReportDataTreeView();
        }

        private void Window_SizeChanged(Object sender, SizeChangedEventArgs e)
        {
            ResizeReportBody();
        }

        private void Window_StateChanged(Object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    ResizeReportBody();
                    break;
                case WindowState.Maximized:
                    ResizeReportBody();
                    break;
            }
        }

        private void NewCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void OpenCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void SaveCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
            var dataSet = new DataSet();
            foreach (DataTable dataTable in DataTableList)
            {
                dataSet.Tables.Add(dataTable);
                SimpleXmlUpdater.Update(dataTable.Rows[0].ItemArray,
                                        String.Format("Xml{0}Reports.xml", Path.DirectorySeparatorChar),
                                        ReportName ?? "Default", "reportTable",
                                        dataTable.TableName, dataTable.Rows[1].ItemArray);
            }
            SimpleXmlUpdater.Update(dataSet, String.Format("Xml{0}Reports.xml", Path.DirectorySeparatorChar),
                                    ReportName ?? "Default");
            dataSet.Tables.Clear();
        }

        private void SaveAsCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void CloseCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void UndoCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void RedoCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void CutCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void CopyCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void PasteCommandHandler(Object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void CanExecuteHandler(Object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void TableMenuItem_Click(Object sender, RoutedEventArgs e)
        {
            if (DataSet == null) return;
            int dataTableCounter = 0;
            var controlTemplate = FindResource("DesignerItemTemplate") as ControlTemplate;
            foreach (
                ContentControl contentControl in from DataTable dataTable in DataSet.Tables select new ContentControl())
            {
                contentControl.Template = controlTemplate;
                var dataGrid = new DataGrid
                    {
                        AllowDrop = true,
                        AutoGenerateColumns = true,
                        CanUserAddRows = false,
                        CanUserDeleteRows = false,
                        ContextMenu = new ContextMenu()
                    };
                dataGrid.DragEnter += dataGrid_DragEnter;
                dataGrid.Drop += dataGrid_Drop;
                dataGrid.LoadingRow += dataGrid_LoadingRow;
                dataGrid.Tag = dataTableCounter;
                var insertColumnMenuItem = new MenuItem {Header = "Insert a Column"};
                insertColumnMenuItem.Click += insertColumnMenuItem_Click;
                var deleteColumnMenuItem = new MenuItem {Header = "Delete a Column"};
                deleteColumnMenuItem.Click += deleteColumnMenuItem_Click;
                var deleteAllColumnMenuItem = new MenuItem {Header = "Delete all Columns"};
                deleteAllColumnMenuItem.Click += deleteAllColumnMenuItem_Click;
                var showColumnsMenuItem = new MenuItem {Header = "Show a Column"};
                dataGrid.ContextMenu.Items.Add(insertColumnMenuItem);
                dataGrid.ContextMenu.Items.Add(deleteColumnMenuItem);
                dataGrid.ContextMenu.Items.Add(deleteAllColumnMenuItem);
                dataGrid.ContextMenu.Items.Add(showColumnsMenuItem);
                var showColumnMenuItem = new MenuItem {Header = "All", IsCheckable = true, IsChecked = true};
                showColumnMenuItem.Checked += showColumnMenuItem_Checked;
                showColumnMenuItem.Unchecked += showColumnMenuItem_Unchecked;
                showColumnsMenuItem.Items.Add(showColumnMenuItem);
                var dt = new DataTable();
                for (int index = 0; index < 3; index++)
                {
                    dt.Columns.Add(new DataColumn("Column" + index, typeof (String)));
                    showColumnMenuItem = new MenuItem {Header = "Column" + index, IsCheckable = true, IsChecked = true};
                    showColumnMenuItem.Checked += showColumnMenuItem_Checked;
                    showColumnMenuItem.Unchecked += showColumnMenuItem_Unchecked;
                    showColumnsMenuItem.Items.Add(showColumnMenuItem);
                }
                dt.Rows.Add(dt.NewRow());
                dt.Rows.Add(dt.NewRow());
                DataTableList.Add(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                contentControl.Content = dataGrid;
                ReportBodyCanvas.Children.Add(contentControl);
                dataTableCounter++;
            }
        }

        private void insertColumnMenuItem_Click(Object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem == null) return;
            var contextMenu = menuItem.Parent as ContextMenu;
            if (contextMenu == null) return;
            var dataGrid = contextMenu.PlacementTarget as DataGrid;
            if (dataGrid == null) return;
            Int32 dataTableCounter;
            if (!Int32.TryParse(dataGrid.Tag.ToString(), out dataTableCounter)) return;
            DataTable dataTable = DataTableList[dataTableCounter];
            dataTable.Columns.Add(new DataColumn(String.Format("Column{0}", dataTable.Columns.Count), typeof (String)));
            var dgtc = new DataGridTextColumn
                {
                    Header = String.Format("Column{0}", dataTable.Columns.Count - 1),
                    Binding = new Binding(String.Format("Column{0}", dataTable.Columns.Count - 1))
                };
            dataGrid.Columns.Add(dgtc);
            if (contextMenu.Items.Count <= 0) return;
            var childMenuItem = contextMenu.Items[contextMenu.Items.Count - 1] as MenuItem;
            if (childMenuItem == null) return;
            var showColumnMenuItem = new MenuItem
                {
                    Header = String.Format("Column{0}", dataTable.Columns.Count - 1),
                    IsCheckable = true,
                    IsChecked = true
                };
            showColumnMenuItem.Checked += showColumnMenuItem_Checked;
            showColumnMenuItem.Unchecked += showColumnMenuItem_Unchecked;
            childMenuItem.Items.Add(showColumnMenuItem);
        }

        private void deleteColumnMenuItem_Click(Object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem == null) return;
            var contextMenu = menuItem.Parent as ContextMenu;
            if (contextMenu == null) return;
            var dataGrid = contextMenu.PlacementTarget as DataGrid;
            if (dataGrid == null) return;
            Int32 dataTableCounter;
            if (!Int32.TryParse(dataGrid.Tag.ToString(), out dataTableCounter)) return;
            DataTable dataTable = DataTableList[dataTableCounter];
            if (dataTable.Columns.Count > 0)
                dataTable.Columns.RemoveAt(dataTable.Columns.Count - 1);
            if (dataGrid.Columns.Count > 0)
                dataGrid.Columns.RemoveAt(dataGrid.Columns.Count - 1);
            if (contextMenu.Items.Count <= 0) return;
            var childMenuItem = contextMenu.Items[contextMenu.Items.Count - 1] as MenuItem;
            if (childMenuItem == null) return;
            if (childMenuItem.Items.Count <= 0) return;
            var mi = childMenuItem.Items[childMenuItem.Items.Count - 1] as MenuItem;
            if (mi == null) return;
            if (!mi.Header.Equals("All"))
                childMenuItem.Items.Remove(mi);
        }

        private void deleteAllColumnMenuItem_Click(Object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem == null) return;
            var contextMenu = menuItem.Parent as ContextMenu;
            if (contextMenu == null) return;
            var dataGrid = contextMenu.PlacementTarget as DataGrid;
            if (dataGrid == null) return;
            Int32 dataTableCounter;
            if (!Int32.TryParse(dataGrid.Tag.ToString(), out dataTableCounter)) return;
            DataTable dataTable = DataTableList[dataTableCounter];
            dataTable.Columns.Clear();
            dataGrid.Columns.Clear();
            if (contextMenu.Items.Count <= 0) return;
            var childMenuItem = contextMenu.Items[contextMenu.Items.Count - 1] as MenuItem;
            if (childMenuItem == null) return;
            for (int index = 0; index < childMenuItem.Items.Count; index++)
            {
                var mi = childMenuItem.Items[index] as MenuItem;
                if (mi == null) continue;
                if (!mi.Header.Equals("All"))
                    childMenuItem.Items.Remove(mi);
            }
        }

        private static void showColumnMenuItem_Checked(Object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem == null) return;
            var parentMenuItem = menuItem.Parent as MenuItem;
            if (parentMenuItem == null) return;
            var contextMenu = parentMenuItem.Parent as ContextMenu;
            if (contextMenu == null) return;
            var dataGrid = contextMenu.PlacementTarget as DataGrid;
            if (dataGrid == null) return;
            if (menuItem.Header.Equals("All"))
            {
                foreach (MenuItem mi in parentMenuItem.Items.OfType<MenuItem>().Where(mi => !mi.Header.Equals("All")))
                {
                    showColumnMenuItem_Checked(mi, null);
                    mi.IsChecked = true;
                }
            }
            else
            {
                foreach (DataGridColumn t in dataGrid.Columns.Where(t => t.Header.Equals(menuItem.Header)))
                {
                    t.Visibility = Visibility.Visible;
                    break;
                }
            }
        }

        private static void showColumnMenuItem_Unchecked(Object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem == null) return;
            var parentMenuItem = menuItem.Parent as MenuItem;
            if (parentMenuItem == null) return;
            var contextMenu = parentMenuItem.Parent as ContextMenu;
            if (contextMenu == null) return;
            var dataGrid = contextMenu.PlacementTarget as DataGrid;
            if (dataGrid == null) return;
            if (menuItem.Header.Equals("All"))
            {
                foreach (MenuItem mi in parentMenuItem.Items.OfType<MenuItem>().Where(mi => !mi.Header.Equals("All")))
                {
                    showColumnMenuItem_Unchecked(mi, null);
                    mi.IsChecked = false;
                }
            }
            else
            {
                foreach (DataGridColumn t in dataGrid.Columns.Where(t => t.Header.Equals(menuItem.Header)))
                {
                    t.Visibility = Visibility.Hidden;
                    break;
                }
            }
        }

        private void treeViewItem_MouseMove(Object sender, MouseEventArgs e)
        {
            var treeViewItem = sender as TreeViewItem;
            if (treeViewItem != null && e.LeftButton == MouseButtonState.Pressed)
                DragDrop.DoDragDrop(treeViewItem, treeViewItem.Header.ToString(), DragDropEffects.Copy);
        }

        private static void dataGrid_DragEnter(Object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof (String)) || sender == e.Source)
                e.Effects = DragDropEffects.None;
        }

        private void dataGrid_Drop(Object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof (String))) return;
            var columnHeader = e.Data.GetData(typeof (String)) as String;
            var dataGrid = sender as DataGrid;
            if (dataGrid == null) return;
            Int32 dataTableCounter;
            if (!Int32.TryParse(dataGrid.Tag.ToString(), out dataTableCounter)) return;
            DataTable dataTable = DataTableList[dataTableCounter];
            if (columnHeader != null && dataTable.Columns.Contains(columnHeader)) return;
            dataTable.Columns.Add(new DataColumn(columnHeader, typeof (String)));
            var dgtc = new DataGridTextColumn {Header = columnHeader, Binding = new Binding(columnHeader)};
            dataGrid.Columns.Add(dgtc);
        }

        private static void dataGrid_LoadingRow(Object sender, DataGridRowEventArgs e)
        {
            switch (e.Row.GetIndex())
            {
                case 0:
                    e.Row.Header = "Header's Name";
                    break;
                case 1:
                    e.Row.Header = "Footer's Name";
                    break;
            }
        }
    }
}