using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClassLibraryReport.Data;
using ClassLibraryReport.Parsers;
using ClassLibraryReport.Renderers;
using ClassLibraryReport.Utils;
using ClassLibraryReport.View;

namespace WindowsFormsReport
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            DataCollection = new List<List<Object>>();
            DataCollection2 = new List<List<Object>>();
            Reports = new Reports();
            SimpleXmlParser = null;
            HtmlRenderer = new HtmlRenderer(Reports, "Css", "JsLib");
        }

        public List<List<Object>> DataCollection { get; private set; }
        public List<List<Object>> DataCollection2 { get; private set; }
        public Reports Reports { get; private set; }
        public SimpleXmlParser SimpleXmlParser { get; private set; }
        public HtmlRenderer HtmlRenderer { get; private set; }

        private void FillReportComboBox()
        {
            var reportNames = new List<String>();
            foreach (Report report in Reports.DataList.Where(report => report.Name != null).
                                              Where(report => !reportNames.Contains(report.Name)))
                reportNames.Add(report.Name);
            reportComboBox.DataSource = reportNames;
            deleteReportComboBox.DataSource = reportNames;
        }

        private void InitReports()
        {
            DataCollection.Add(new List<Object>
                {
                    "Hercule Pirlot",
                    "Josianne Poirot",
                    "http://96.img.v4.skyrock.net/8182/33708182/pics/2669555276_small_1.jpg",
                    "Vol en série",
                    54544.44d
                });
            DataCollection.Add(new List<Object>
                {
                    "Marc Herman",
                    "Jean Paul Rouchier",
                    "Flamand Lenoir",
                    "http://www.justacote.com/photos_entreprises/cabinet-oger-avocat-vitre-1265641794.jpg",
                    444.44d
                });
            DataCollection.Add(new List<Object>
                {
                    "Edgar Alan Poe",
                    "Zlatan Ibrahimovich",
                    "John Doe",
                    "http://www.bonjourmadame.fr/",
                    8574.44d
                });
            DataCollection2.Add(new List<Object>
                {
                    "Friedrich Van perterman",
                    "Rocco Siffredi",
                    "http://www.geekandgame.com/wp-content/uploads/2012/06/4bb.gif",
                    "http://www.youtube.com/watch?v=oavMtUWDBTM"
                });
            DataCollection2.Add(new List<Object>
                {
                    "Michel Michel",
                    "Jacky Jacky",
                    "Herman van pertersurg",
                    "http://www.siteduzero.com/informatique/tutoriels/apprenez-a-developper-en-c/les-tests-unitaires-1"
                });
        }

        private void RefreshReportForm()
        {
            SimpleXmlUpdater.Update(DataCollection[0], String.Format("Xml{0}Reports.xml", Path.DirectorySeparatorChar),
                                    "Affaire 254", "dataCollection");
            SimpleXmlUpdater.Update(DataCollection2[0], String.Format("Xml{0}Reports.xml", Path.DirectorySeparatorChar),
                                    "Rapport du 54", "dataCollection2");
            Reports = new Reports(new List<Report>
                {
                    ObjConverter.Convert(DataCollection),
                    ObjConverter.Convert(DataCollection2)
                });
            SimpleXmlParser = new SimpleXmlParser(String.Format("Xml{0}Reports.xml", Path.DirectorySeparatorChar),
                                                  String.Format("Schemas{0}Reports.xsd", Path.DirectorySeparatorChar),
                                                  Reports);
            SimpleXmlParser.Parse();
            HtmlRenderer.Reports = Reports;
            HtmlRenderer.Render();
            webBrowser.Refresh();
            FillReportComboBox();
        }

        private void ReportForm_Load(Object sender, EventArgs e)
        {
            //this.Reports = Serializer.DeSerializeObject("SerializedReports.xml") as Reports<string>;
            if (Reports == null)
                Reports = new Reports();
            if (Reports.IsDataListEmpty())
                InitReports();
            RefreshReportForm();
        }

        private void ReportForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            //Serializer.SerializeObject("SerializedReports.xml", this.Reports);
        }

        private void addReportButton_Click(Object sender, EventArgs e)
        {
            var report = new Report(addTitleTextBox.Text);
            var dataSet = new DataSet();
            var fields = new Fields();
            fields.AddData(new Field(nameTextBox.Text));
            fields.AddData(new Field(typeTextBox.Text));
            dataSet.Fieldss.AddData(fields);
            report.DataSets.AddData(dataSet);
            Reports.AddData(report);
            RefreshReportForm();
        }

        private void deleteReportButton_Click(Object sender, EventArgs e)
        {
            if (reportCheckBox.CheckState == CheckState.Checked)
            {
                Reports.RemoveAllData();
                RefreshReportForm();
            }
            else if (deleteReportComboBox.SelectedItem != null)
            {
                Reports.RemoveDataByName(deleteReportComboBox.SelectedItem as String);
                RefreshReportForm();
            }
        }

        private void modifyButton_Click(Object sender, EventArgs e)
        {
            var report = new Report(modifyTitleTextBox.Text);
            var dataSet = new DataSet();
            var fields = new Fields();
            fields.AddData(new Field(newNameTextBox.Text));
            fields.AddData(new Field(newTypeTextBox.Text));
            dataSet.Fieldss.AddData(fields);
            report.DataSets.AddData(dataSet);
            Reports.ModifyData(oldNameTextBox.Text, report);
            RefreshReportForm();
        }

        private void reportComboBox_SelectedIndexChanged(Object sender, EventArgs e)
        {
            webBrowser.Navigate(new Uri(string.Format("{0}{1}{2}.html", Directory.GetCurrentDirectory(),
                                                      Path.DirectorySeparatorChar, reportComboBox.SelectedItem)));
        }
    }
}