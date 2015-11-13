using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using ClassLibraryReport.Data;
using ClassLibraryReport.Interfaces;
using ClassLibraryReport.Utils;
using ClassLibraryReport.View;

namespace ClassLibraryReport.Renderers
{
    public class HtmlRenderer : Renderer, IHtmlRenderer
    {
        public HtmlRenderer() : this(default(Reports))
        {
        }

        public HtmlRenderer(Report report) : this(report, null)
        {
        }

        public HtmlRenderer(Reports reports) : this(reports, null)
        {
        }

        public HtmlRenderer(Report report, String cssDirectoryPath) :
            this(report, cssDirectoryPath, null)
        {
        }

        public HtmlRenderer(Reports reports, String cssDirectoryPath) :
            this(reports, cssDirectoryPath, null)
        {
        }

        public HtmlRenderer(Report report, String cssDirectoryPath, String jsDirectoryPath) :
            this(report, cssDirectoryPath, jsDirectoryPath, null)
        {
        }

        public HtmlRenderer(Reports reports, String cssDirectoryPath, String jsDirectoryPath) :
            this(reports, cssDirectoryPath, jsDirectoryPath, null)
        {
        }

        public HtmlRenderer(Report report, String cssDirectoryPath, String jsDirectoryPath,
                            String translationsFilePath) :
                                this(report, cssDirectoryPath, jsDirectoryPath, translationsFilePath, null)
        {
        }

        public HtmlRenderer(Reports reports, String cssDirectoryPath, String jsDirectoryPath,
                            String translationsFilePath) :
                                this(reports, cssDirectoryPath, jsDirectoryPath, translationsFilePath, null)
        {
        }

        public HtmlRenderer(Report report, String cssDirectoryPath, String jsDirectoryPath,
                            String translationsFilePath, String htmlDirectoryPath) :
                                this(
                                report, cssDirectoryPath, jsDirectoryPath, translationsFilePath, htmlDirectoryPath, null
                                )
        {
        }

        public HtmlRenderer(Reports reports, String cssDirectoryPath, String jsDirectoryPath,
                            String translationsFilePath, String htmlDirectoryPath) :
                                this(
                                reports, cssDirectoryPath, jsDirectoryPath, translationsFilePath, htmlDirectoryPath,
                                null)
        {
        }

        public HtmlRenderer(Report report, String cssDirectoryPath, String jsDirectoryPath,
                            String translationsFilePath, String htmlDirectoryPath, String mhtmlDirectoryPath) :
                                this(new Reports(new List<Report> {report}), cssDirectoryPath, jsDirectoryPath,
                                     translationsFilePath, htmlDirectoryPath, mhtmlDirectoryPath)
        {
        }

        public HtmlRenderer(Reports reports, String cssDirectoryPath, String jsDirectoryPath,
                            String translationsFilePath, String htmlDirectoryPath, String mhtmlDirectoryPath) :
                                base(reports)
        {
            CssDirectoryPath = cssDirectoryPath;
            JsDirectoryPath = jsDirectoryPath;
            TranslationsDictionary = TranslationsHelper.Help(translationsFilePath);
            CachedGroupValue = "";
            IsGroupEnded = false;
            GroupCounter = -1;
            RowCounter = 0;
            HtmlDirectoryPath = htmlDirectoryPath;
            MhtmlDirectoryPath = mhtmlDirectoryPath;
        }

        public HtmlRenderer(HtmlRenderer htmlRenderer) : base(htmlRenderer)
        {
            CssDirectoryPath = htmlRenderer.CssDirectoryPath;
            JsDirectoryPath = htmlRenderer.JsDirectoryPath;
            TranslationsDictionary = new Dictionary<String, String>(htmlRenderer.TranslationsDictionary);
            CachedGroupValue = htmlRenderer.CachedGroupValue;
            IsGroupEnded = htmlRenderer.IsGroupEnded;
            GroupCounter = htmlRenderer.GroupCounter;
            RowCounter = htmlRenderer.RowCounter;
            HtmlDirectoryPath = htmlRenderer.HtmlDirectoryPath;
            MhtmlDirectoryPath = htmlRenderer.MhtmlDirectoryPath;
        }

        public String CssDirectoryPath { get; set; }
        public String JsDirectoryPath { get; set; }
        public Dictionary<String, String> TranslationsDictionary { get; set; }
        public Object CachedGroupValue { get; set; }
        public Boolean IsGroupEnded { get; set; }
        public Int32 GroupCounter { get; set; }
        public Int32 RowCounter { get; set; }
        public String HtmlDirectoryPath { get; set; }
        public String MhtmlDirectoryPath { get; set; }

        public override void Render()
        {
            Render(true);
        }

        public void Render(Boolean doMhtml)
        {
            if (Reports == null || Reports.IsDataListEmpty()) return;
            foreach (
                Report report in Reports.DataList.Where(report => report != null && !String.IsNullOrEmpty(report.Name)))
            {
                String htmlPath;
                if (!String.IsNullOrEmpty(HtmlDirectoryPath) && Directory.Exists(HtmlDirectoryPath))
                    htmlPath = String.Format("{0}{1}{2}{3}{4}.html", Directory.GetCurrentDirectory(),
                                             Path.DirectorySeparatorChar, HtmlDirectoryPath, Path.DirectorySeparatorChar,
                                             report.Name);
                else
                    htmlPath = String.Format("{0}{1}{2}.html", Directory.GetCurrentDirectory(),
                                             Path.DirectorySeparatorChar, report.Name);
                var writer = new HtmlTextWriter(new StreamWriter(htmlPath));
                writer.Write("<!DOCTYPE html>");
                writer.RenderBeginTag(HtmlTextWriterTag.Html);
                writer.RenderBeginTag(HtmlTextWriterTag.Head);
                writer.RenderBeginTag(HtmlTextWriterTag.Title);
                writer.Write(report.Name);
                writer.RenderEndTag();
                writer.AddAttribute("http-equiv", "Content-Type");
                writer.AddAttribute(HtmlTextWriterAttribute.Content, "text/html; charset=utf-8");
                writer.RenderBeginTag(HtmlTextWriterTag.Meta);
                writer.RenderEndTag();
                LoadCss(ref writer);
                LoadJs(ref writer);
                RenderJs(ref writer, report.Body);
                RenderJs(ref writer, report.PageHeader);
                RenderJs(ref writer, report.PageFooter);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.Body);
                if (report.DisplayTitle)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.H1);
                    writer.Write(report.Name);
                    writer.RenderEndTag();
                }
                RenderContainer(ref writer, report.PageHeader);
                RenderContainer(ref writer, report.Body);
                RenderContainer(ref writer, report.PageFooter);
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.Flush();
                writer.Close();
                if (!doMhtml) continue;
                if (!String.IsNullOrEmpty(MhtmlDirectoryPath) && Directory.Exists(MhtmlDirectoryPath))
                    MhtConverter.Convert(htmlPath,
                                         String.Format("{0}{1}{2}{3}{4}.mhtml", Directory.GetCurrentDirectory(),
                                                       Path.DirectorySeparatorChar, MhtmlDirectoryPath,
                                                       Path.DirectorySeparatorChar, report.Name));
                else
                    MhtConverter.Convert(htmlPath, String.Format("{0}{1}{2}.mhtml", Directory.GetCurrentDirectory(),
                                                                 Path.DirectorySeparatorChar, report.Name));
            }
        }

        public void LoadCss(ref HtmlTextWriter writer)
        {
            if (String.IsNullOrEmpty(CssDirectoryPath) || !Directory.Exists(CssDirectoryPath)) return;
            foreach (string directory in Directory.GetDirectories(CssDirectoryPath))
            {
                string directoryInfoName = new DirectoryInfo(directory).Name;
                IEnumerable<string> cssFiles = Directory.EnumerateFiles(String.Format("{0}{1}{2}", CssDirectoryPath,
                                                                                      Path.DirectorySeparatorChar,
                                                                                      directoryInfoName), "*.css");
                foreach (string cssFile in cssFiles)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
                    if (!String.IsNullOrEmpty(HtmlDirectoryPath) && Directory.Exists(HtmlDirectoryPath))
                        writer.AddAttribute(HtmlTextWriterAttribute.Href,
                                            String.Format("..{0}{1}", Path.DirectorySeparatorChar, cssFile));
                    else
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, cssFile);
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
                    writer.AddAttribute("media", directoryInfoName);
                    writer.RenderBeginTag(HtmlTextWriterTag.Link);
                    writer.RenderEndTag();
                }
            }
        }

        public void LoadJs(ref HtmlTextWriter writer)
        {
            if (String.IsNullOrEmpty(JsDirectoryPath) || !Directory.Exists(JsDirectoryPath)) return;
            foreach (string jsFile in Directory.GetDirectories(JsDirectoryPath).Select(
                directory => Directory.EnumerateFiles(String.Format("{0}{1}{2}", JsDirectoryPath,
                                                                    Path.DirectorySeparatorChar,
                                                                    new DirectoryInfo(directory).Name), "*.js")).
                                                SelectMany(jsFiles => jsFiles))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                if (!String.IsNullOrEmpty(HtmlDirectoryPath) && Directory.Exists(HtmlDirectoryPath))
                    writer.AddAttribute(HtmlTextWriterAttribute.Src,
                                        String.Format("..{0}{1}", Path.DirectorySeparatorChar, jsFile));
                else
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, jsFile);
                writer.RenderBeginTag(HtmlTextWriterTag.Script);
                writer.RenderEndTag();
            }
        }

        public void RenderJs(ref HtmlTextWriter writer, Container container)
        {
            if (container == null || container.IsDataListEmpty()) return;
            foreach (IDisplayable displayable in container.DataList.Where(displayable => displayable != null))
                if (displayable is Tablix && (displayable as Tablix).Dynamic)
                {
                    var tablix = displayable as Tablix;
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                    writer.RenderBeginTag(HtmlTextWriterTag.Script);
                    string jsLine = "$(document).ready(function(){";
                    string tablixId = tablix.Style != null ? RenderStyle(tablix.Style) : "#reportTable";
                    if (tablix.TablixChart != null)
                        jsLine += String.Format("$('{0}').highchartTable();", tablixId);
                    if (tablix.TablixColumns != null)
                    {
                        jsLine += String.Format("$('{0}').dataTable({{", tablixId);
                        if (tablix.TablixColumns.Hideable)
                            jsLine += "'sDom':'RC<\"clear\">lfrtip',";
                        if (tablix.TablixColumns.Reorderable && !tablix.TablixColumns.Hideable)
                            jsLine += "'sDom':'Rlfrtip',";
                        if (!String.IsNullOrEmpty(tablix.PaginationType))
                            jsLine += String.Format("'sPaginationType':'{0}',", tablix.PaginationType);
                        if (!String.IsNullOrEmpty(tablix.TablixColumns.Sorting))
                            if (tablix.Paginate)
                                jsLine += String.Format("bPaginate:true,'aaSorting':[{0}],",
                                                        tablix.TablixColumns.Sorting);
                            else
                                jsLine += String.Format("bPaginate:false,'aaSorting':[{0}],",
                                                        tablix.TablixColumns.Sorting);
                        jsLine += "})";
                    }
                    else
                    {
                        jsLine += String.Format("$('{0}').dataTable({{", tablixId);
                        if (!String.IsNullOrEmpty(tablix.PaginationType))
                            jsLine += String.Format("'sPaginationType':'{0}',", tablix.PaginationType);
                        if (tablix.Paginate)
                            jsLine += "bPaginate:true,";
                        else
                            jsLine += "bPaginate:false,";
                        jsLine += "})";
                    }
                    if (tablix.TablixColumns != null && tablix.TablixColumns.TablixColumnsGrouping != null)
                    {
                        jsLine += ".rowGrouping({";
                        if (!String.IsNullOrEmpty(tablix.TablixColumns.TablixColumnsGrouping.ColumnIndex))
                            jsLine += String.Format("iGroupingColumnIndex:{0},",
                                                    tablix.TablixColumns.TablixColumnsGrouping.ColumnIndex);
                        if (!String.IsNullOrEmpty(tablix.TablixColumns.TablixColumnsGrouping.ColumnSortDirection))
                            jsLine += String.Format("sGroupingColumnSortDirection:{0},",
                                                    tablix.TablixColumns.TablixColumnsGrouping.ColumnSortDirection);
                        if (!String.IsNullOrEmpty(tablix.TablixColumns.TablixColumnsGrouping.OrderByColumnIndex))
                            jsLine += String.Format("iGroupingOrderByColumnIndex:{0},",
                                                    tablix.TablixColumns.TablixColumnsGrouping.OrderByColumnIndex);
                        if (!String.IsNullOrEmpty(tablix.TablixColumns.TablixColumnsGrouping.Class))
                            jsLine += String.Format("sGroupingClass:{0},",
                                                    tablix.TablixColumns.TablixColumnsGrouping.Class);
                        if (!String.IsNullOrEmpty(tablix.TablixColumns.TablixColumnsGrouping.Expandable))
                            jsLine += String.Format("bExpandableGrouping:{0},",
                                                    tablix.TablixColumns.TablixColumnsGrouping.Expandable);
                        if (!String.IsNullOrEmpty(tablix.TablixColumns.TablixColumnsGrouping.ExpandSingle))
                            jsLine += String.Format("bExpandSingleGroup:{0},",
                                                    tablix.TablixColumns.TablixColumnsGrouping.ExpandSingle);
                        if (!String.IsNullOrEmpty(tablix.TablixColumns.TablixColumnsGrouping.DateFormat))
                            jsLine += String.Format("sDateFormat:{0},",
                                                    tablix.TablixColumns.TablixColumnsGrouping.DateFormat);
                        jsLine += "});";
                    }
                    jsLine += "});";
                    writer.Write(jsLine);
                    writer.RenderEndTag();
                }
                else if (displayable is Box)
                    RenderJs(ref writer, displayable as Box);
        }

        public void RenderContainer(ref HtmlTextWriter writer, Container container)
        {
            if (container == null) return;
            if (!String.IsNullOrEmpty(container.Name))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, container.Name);
            if (!String.IsNullOrEmpty(container.Style))
                RenderStyle(ref writer, container.Style);
            if (!String.IsNullOrEmpty(container.Tag))
                writer.RenderBeginTag(container.Tag.Equals("P", StringComparison.OrdinalIgnoreCase)
                                          ? HtmlTextWriterTag.P
                                          : HtmlTextWriterTag.Div);
            else
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (!container.IsDataListEmpty())
                foreach (IDisplayable displayable in container.DataList.Where(displayable => displayable != null))
                    if (displayable is TextBox)
                        RenderTextBox(ref writer, displayable as TextBox);
                    else if (displayable is Tablix)
                        RenderTablix(ref writer, displayable as Tablix);
                    else if (displayable is Chart)
                        RenderChart(ref writer, displayable as Chart);
                    else if (displayable is Box)
                        RenderContainer(ref writer, displayable as Box);
            writer.RenderEndTag();
        }

        public void RenderTablix(ref HtmlTextWriter writer, Tablix tablix)
        {
            if (!String.IsNullOrEmpty(tablix.Name))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, tablix.Name);
            if (!String.IsNullOrEmpty(tablix.Style))
                RenderStyle(ref writer, tablix.Style);
            else
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "reportTable");
            if (tablix.TablixChart != null)
            {
                writer.AddAttribute("data-graph-type", !String.IsNullOrEmpty(tablix.TablixChart.Type)
                                                           ? tablix.TablixChart.Type
                                                           : "column");
                if (!String.IsNullOrEmpty(tablix.TablixChart.Subtitle))
                    writer.AddAttribute("data-graph-subtitle-text", tablix.TablixChart.Subtitle);
                if (!String.IsNullOrEmpty(tablix.TablixChart.Inverted))
                    writer.AddAttribute("data-graph-inverted", tablix.TablixChart.Inverted);
                if (!String.IsNullOrEmpty(tablix.TablixChart.Height))
                    writer.AddAttribute("data-graph-height", tablix.TablixChart.Height);
                if (!String.IsNullOrEmpty(tablix.TablixChart.Container))
                    writer.AddAttribute(tablix.TablixChart.Container.StartsWith("#")
                                            ? "data-graph-container"
                                            : "data-graph-container-before", tablix.TablixChart.Container);
                if (!String.IsNullOrEmpty(tablix.TablixChart.FillOpacity))
                    writer.AddAttribute("data-graph-area-fillOpacity", tablix.TablixChart.FillOpacity);
                if (!String.IsNullOrEmpty(tablix.TablixChart.LegendDisabled))
                    writer.AddAttribute("data-graph-legend-disabled", tablix.TablixChart.LegendDisabled);
                if (!String.IsNullOrEmpty(tablix.TablixChart.LegendLayout))
                    writer.AddAttribute("data-graph-legend-layout", tablix.TablixChart.LegendLayout);
                if (!String.IsNullOrEmpty(tablix.TablixChart.LegendWidth))
                    writer.AddAttribute("data-graph-legend-width", tablix.TablixChart.LegendWidth);
                if (!String.IsNullOrEmpty(tablix.TablixChart.LegendX))
                    writer.AddAttribute("data-graph-legend-x", tablix.TablixChart.LegendX);
                if (!String.IsNullOrEmpty(tablix.TablixChart.LegendY))
                    writer.AddAttribute("data-graph-legend-y", tablix.TablixChart.LegendY);
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            if (!String.IsNullOrEmpty(tablix.Caption))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Caption);
                writer.Write(tablix.Caption);
                writer.RenderEndTag();
            }
            RenderTablixContainer(ref writer, tablix.TablixHeader, tablix.DataSet, tablix.TablixColumnHierarchy);
            RenderTablixContainer(ref writer, tablix.TablixBody, tablix.DataSet, tablix.TablixColumnHierarchy);
            RenderTablixContainer(ref writer, tablix.TablixFooter, tablix.DataSet, tablix.TablixColumnHierarchy);
            writer.RenderEndTag();
        }

        public void RenderTablixContainer(ref HtmlTextWriter writer, TablixContainer tablixContainer,
                                          DataSet dataSet, TablixColumnHierarchy tablixColumnHierarchy)
        {
            if (tablixContainer == null || tablixContainer.TablixRows == null ||
                tablixContainer.TablixRows.IsDataListEmpty()) return;
            if (tablixContainer is TablixBody)
                writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
            else if (tablixContainer is TablixHeader)
                writer.RenderBeginTag(HtmlTextWriterTag.Thead);
            else if (tablixContainer is TablixFooter)
                writer.RenderBeginTag(HtmlTextWriterTag.Tfoot);
            if (dataSet != null && dataSet.Fieldss != null && !dataSet.Fieldss.IsDataListEmpty() &&
                dataSet.FieldDescriptors != null && !dataSet.FieldDescriptors.IsDataListEmpty() &&
                dataSet.Stats != null && tablixContainer.Linked)
                foreach (Fields fields in dataSet.Fieldss.DataList.Where(
                    fields => fields != null && !fields.IsDataListEmpty()))
                    foreach (TablixRow tablixRow in tablixContainer.TablixRows.DataList.Where(
                        tablixRow => tablixRow != null && tablixRow.TablixCells != null &&
                                     !tablixRow.TablixCells.IsDataListEmpty()))
                        RenderTablixRow(ref writer, tablixRow, fields, tablixContainer,
                                        tablixColumnHierarchy, dataSet);
            else if (dataSet != null && dataSet.FieldDescriptors != null &&
                     !dataSet.FieldDescriptors.IsDataListEmpty() && dataSet.Stats != null)
                foreach (TablixRow tablixRow in tablixContainer.TablixRows.DataList.Where(
                    tablixRow => tablixRow != null && tablixRow.TablixCells != null &&
                                 !tablixRow.TablixCells.IsDataListEmpty()))
                    RenderTablixRow(ref writer, tablixRow, null, tablixContainer, null, dataSet);
            else
                foreach (TablixRow tablixRow in tablixContainer.TablixRows.DataList.Where(
                    tablixRow => tablixRow != null && tablixRow.TablixCells != null &&
                                 !tablixRow.TablixCells.IsDataListEmpty()))
                    RenderTablixRow(ref writer, tablixRow, null, tablixContainer, null, null);
            writer.RenderEndTag();
        }

        public void RenderTablixRow(ref HtmlTextWriter writer, TablixRow tablixRow, Fields fields,
                                    TablixContainer tablixContainer, TablixColumnHierarchy tablixColumnHierarchy,
                                    DataSet dataSet)
        {
            if (!String.IsNullOrEmpty(tablixRow.Name))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, tablixRow.Name);
            if (!String.IsNullOrEmpty(tablixRow.Style))
                RenderStyle(ref writer, tablixRow.Style);
            if (tablixColumnHierarchy != null && tablixColumnHierarchy.Group != null &&
                tablixColumnHierarchy.Group.On != null)
            {
                if (!CachedGroupValue.Equals(
                    fields.DataList[dataSet.FieldDescriptors.ContainsDataById(tablixColumnHierarchy.Group.On)].Value))
                {
                    if (!fields.DataList[dataSet.FieldDescriptors.ContainsDataById(
                        tablixColumnHierarchy.Group.On)].Value.Equals("_END_GROUP_"))
                    {
                        IsGroupEnded = true;
                        GroupCounter++;
                        if (!CachedGroupValue.Equals(""))
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                            writer.AddAttribute(HtmlTextWriterAttribute.Colspan,
                                                dataSet.FieldDescriptors.DataList.Count.ToString(
                                                    CultureInfo.InvariantCulture));
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                            writer.RenderBeginTag(HtmlTextWriterTag.Table);
                            if (tablixColumnHierarchy.TablixFooter != null)
                            {
                                Stats statsBackup = dataSet.Stats;
                                dataSet.Stats = dataSet.Statss.DataList[GroupCounter - 1];
                                RenderTablixContainer(ref writer, tablixColumnHierarchy.TablixFooter, dataSet, null);
                                dataSet.Stats = statsBackup;
                            }
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                        }
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        writer.AddAttribute(HtmlTextWriterAttribute.Colspan,
                                            dataSet.FieldDescriptors.DataList.Count.ToString(
                                                CultureInfo.InvariantCulture));
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                        writer.RenderBeginTag(HtmlTextWriterTag.Table);
                        if (tablixColumnHierarchy.TablixHeader != null)
                        {
                            Stats statsBackup = dataSet.Stats;
                            dataSet.Stats = dataSet.Statss.DataList[GroupCounter];
                            RenderTablixContainer(ref writer, tablixColumnHierarchy.TablixHeader, dataSet, null);
                            dataSet.Stats = statsBackup;
                        }
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        foreach (
                            TablixCell tablixCell in
                                tablixRow.TablixCells.DataList.Where(tablixCell => tablixCell != null))
                            RenderTablixCell(ref writer, tablixCell, fields, tablixContainer, dataSet.FieldDescriptors,
                                             dataSet.Statss.DataList[GroupCounter]);
                        writer.RenderEndTag();
                        if (dataSet.Fieldss.DataList.Count() - dataSet.Statss.DataList.Count <= 3 &&
                            dataSet.Statss.DataList.Count - 1 == GroupCounter)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                            writer.AddAttribute(HtmlTextWriterAttribute.Colspan,
                                                dataSet.FieldDescriptors.DataList.Count.ToString(
                                                    CultureInfo.InvariantCulture));
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                            writer.RenderBeginTag(HtmlTextWriterTag.Table);
                            if (tablixColumnHierarchy.TablixFooter != null)
                            {
                                Stats statsBackup = dataSet.Stats;
                                dataSet.Stats = dataSet.Statss.DataList[GroupCounter];
                                RenderTablixContainer(ref writer, tablixColumnHierarchy.TablixFooter, dataSet, null);
                                dataSet.Stats = statsBackup;
                            }
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                        }
                    }
                }
                else
                {
                    if (!IsGroupEnded)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        foreach (
                            TablixCell tablixCell in
                                tablixRow.TablixCells.DataList.Where(tablixCell => tablixCell != null))
                            RenderTablixCell(ref writer, tablixCell, fields, tablixContainer, dataSet.FieldDescriptors,
                                             dataSet.Statss.DataList[GroupCounter]);
                        writer.RenderEndTag();
                        if (RowCounter >= dataSet.Fieldss.DataList.Count() - dataSet.Statss.DataList.Count)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                            writer.AddAttribute(HtmlTextWriterAttribute.Colspan,
                                                dataSet.FieldDescriptors.DataList.Count.ToString(
                                                    CultureInfo.InvariantCulture));
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                            writer.RenderBeginTag(HtmlTextWriterTag.Table);
                            if (tablixColumnHierarchy.TablixFooter != null)
                            {
                                Stats statsBackup = dataSet.Stats;
                                dataSet.Stats = dataSet.Statss.DataList[GroupCounter];
                                RenderTablixContainer(ref writer, tablixColumnHierarchy.TablixFooter, dataSet, null);
                                dataSet.Stats = statsBackup;
                            }
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                        }
                    }
                    else
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        foreach (
                            TablixCell tablixCell in
                                tablixRow.TablixCells.DataList.Where(tablixCell => tablixCell != null))
                            RenderTablixCell(ref writer, tablixCell, fields, tablixContainer, dataSet.FieldDescriptors,
                                             dataSet.Statss.DataList[GroupCounter]);
                        writer.RenderEndTag();
                        if (RowCounter >= dataSet.Fieldss.DataList.Count() - dataSet.Statss.DataList.Count)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                            writer.AddAttribute(HtmlTextWriterAttribute.Colspan,
                                                dataSet.FieldDescriptors.DataList.Count.ToString(
                                                    CultureInfo.InvariantCulture));
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                            writer.RenderBeginTag(HtmlTextWriterTag.Table);
                            if (tablixColumnHierarchy.TablixFooter != null)
                            {
                                Stats statsBackup = dataSet.Stats;
                                dataSet.Stats = dataSet.Statss.DataList[GroupCounter];
                                RenderTablixContainer(ref writer, tablixColumnHierarchy.TablixFooter, dataSet, null);
                                dataSet.Stats = statsBackup;
                            }
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                        }
                        IsGroupEnded = false;
                    }
                }
                CachedGroupValue =
                    fields.DataList[dataSet.FieldDescriptors.ContainsDataById(tablixColumnHierarchy.Group.On)].Value;
                RowCounter++;
            }
            else
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                if (dataSet != null)
                    foreach (
                        TablixCell tablixCell in tablixRow.TablixCells.DataList.Where(tablixCell => tablixCell != null))
                        RenderTablixCell(ref writer, tablixCell, fields, tablixContainer, dataSet.FieldDescriptors,
                                         dataSet.Stats);
                else
                    foreach (
                        TablixCell tablixCell in tablixRow.TablixCells.DataList.Where(tablixCell => tablixCell != null))
                        RenderTablixCell(ref writer, tablixCell, fields, tablixContainer, null, null);
                writer.RenderEndTag();
            }
        }

        public void RenderTablixCell(ref HtmlTextWriter writer, TablixCell tablixCell, Fields fields,
                                     TablixContainer tablixContainer, FieldDescriptors fieldDescriptors, Stats stats)
        {
            int columnIndex = -1;
            if (fieldDescriptors != null && tablixCell.Id != null)
                columnIndex = fieldDescriptors.ContainsDataById(tablixCell.Id);
            Field field = null;
            if (fields != null && !fields.IsDataListEmpty())
                field = columnIndex != -1 ? fields.DataList[columnIndex] : null;
            if (!String.IsNullOrEmpty(tablixCell.Name))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, tablixCell.Name);
            if (!String.IsNullOrEmpty(tablixCell.Style))
                RenderStyle(ref writer, tablixCell.Style);
            if (!String.IsNullOrEmpty(tablixCell.RowSpan))
                writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, tablixCell.RowSpan);
            if (!String.IsNullOrEmpty(tablixCell.ColSpan))
                writer.AddAttribute(HtmlTextWriterAttribute.Colspan, tablixCell.ColSpan);
            writer.RenderBeginTag(tablixContainer is TablixHeader ? HtmlTextWriterTag.Th : HtmlTextWriterTag.Td);
            if (field != null && field.Value != null && fieldDescriptors != null &&
                !fieldDescriptors.IsDataListEmpty())
            {
                if (tablixCell.TextBox != null)
                    tablixCell.TextBox.Value = field.Value;
                else
                    tablixCell.TextBox = new TextBox(null, field.Value);
                if (field.Value.GetType() == Type.GetType(fieldDescriptors.DataList[columnIndex].Type))
                    RenderTextBox(ref writer, tablixCell.TextBox, null, true);
                else
                    writer.Write("Bad Type :/ (Given: {0} Expected: {1} Value: {2})",
                                 field.Value.GetType(), fieldDescriptors.DataList[columnIndex].Type, field.Value);
            }
            else if (tablixCell.TextBox != null)
            {
                Object statsObject = null;
                if (fieldDescriptors != null && stats != null)
                    statsObject = RenderStats(tablixCell, fieldDescriptors, stats);
                var textBox = new TextBox(tablixCell.TextBox);
                if (statsObject != null)
                    textBox.Value = statsObject;
                RenderTextBox(ref writer, textBox, null, true);
            }
            writer.RenderEndTag();
        }

        public Object RenderStats(TablixCell tablixCell, FieldDescriptors fieldDescriptors, Stats stats)
        {
            if (tablixCell == null || tablixCell.TextBox == null || tablixCell.TextBox.Value == null ||
                !(tablixCell.TextBox.Value is String) || fieldDescriptors == null ||
                fieldDescriptors.IsDataListEmpty() || stats == null) return null;
            var textBoxValue = tablixCell.TextBox.Value as String;
            if (!textBoxValue.StartsWith("=")) return null;
            int aggregateFunctionLength = textBoxValue.IndexOf("(", StringComparison.Ordinal) - 1;
            if (aggregateFunctionLength <= 0) return null;
            string aggregateFunction = textBoxValue.Substring(1, aggregateFunctionLength);
            if (!StatsHelper.AggregateFunctions.Contains(aggregateFunction)) return null;
            int aggregateArgumentLength = textBoxValue.IndexOf(")", StringComparison.Ordinal) -
                                          textBoxValue.IndexOf("(", StringComparison.Ordinal) - 1;
            if (aggregateArgumentLength <= 0) return null;
            string aggregateArgument = textBoxValue.Substring(
                textBoxValue.IndexOf("(", StringComparison.Ordinal) + 1, aggregateArgumentLength);
            Object value = null;
            int columnIndex = fieldDescriptors.ContainsDataById(aggregateArgument);
            if (columnIndex < 0) return null;
            switch (aggregateFunction)
            {
                case "FIRST":
                    value = stats.Firsts != null && !stats.Firsts.IsDataListEmpty()
                                ? (columnIndex != -1 ? stats.Firsts.DataList[columnIndex] : null)
                                : "N/A";
                    break;
                case "LAST":
                    value = stats.Lasts != null && !stats.Lasts.IsDataListEmpty()
                                ? (columnIndex != -1 ? stats.Lasts.DataList[columnIndex] : null)
                                : "N/A";
                    break;
                case "AVG":
                    value = stats.Avgs != null && !stats.Avgs.IsDataListEmpty()
                                ? (columnIndex != -1 ? stats.Avgs.DataList[columnIndex] : null)
                                : "N/A";
                    break;
                case "MIN":
                    value = stats.Mins != null && !stats.Mins.IsDataListEmpty()
                                ? (columnIndex != -1 ? stats.Mins.DataList[columnIndex] : null)
                                : "N/A";
                    break;
                case "MAX":
                    value = stats.Maxs != null && !stats.Maxs.IsDataListEmpty()
                                ? (columnIndex != -1 ? stats.Maxs.DataList[columnIndex] : null)
                                : "N/A";
                    break;
                case "SUM":
                    value = stats.Sums != null && !stats.Sums.IsDataListEmpty()
                                ? (columnIndex != -1 ? stats.Sums.DataList[columnIndex] : null)
                                : "N/A";
                    break;
                case "COUNT":
                    value = stats.Counts != null && !stats.Counts.IsDataListEmpty()
                                ? (columnIndex != -1 ? stats.Counts.DataList[columnIndex] : null)
                                : "N/A";
                    break;
                case "STDEV":
                    value = stats.StDevs != null && !stats.StDevs.IsDataListEmpty()
                                ? (columnIndex != -1 ? stats.StDevs.DataList[columnIndex] : null)
                                : "N/A";
                    break;
                case "VAR":
                    value = stats.Vars != null && !stats.Vars.IsDataListEmpty()
                                ? (columnIndex != -1 ? stats.Vars.DataList[columnIndex] : null)
                                : "N/A";
                    break;
            }
            return value;
        }

        public void RenderTextBox(ref HtmlTextWriter writer, TextBox textBox)
        {
            RenderTextBox(ref writer, textBox, HtmlTextWriterTag.P);
        }

        public void RenderTextBox(ref HtmlTextWriter writer, TextBox textBox, HtmlTextWriterTag? tag)
        {
            RenderTextBox(ref writer, textBox, tag, true);
        }

        public void RenderTextBox(ref HtmlTextWriter writer, TextBox textBox, HtmlTextWriterTag? tag, Boolean useStyle)
        {
            if (textBox.Value is String && IsValidUri(textBox.Value as String))
                if ((textBox.Value as String).Contains(".jpg") ||
                    (textBox.Value as String).Contains(".png") ||
                    (textBox.Value as String).Contains(".gif"))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, textBox.Value as String);
                    if (!String.IsNullOrEmpty(textBox.Name))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Name, textBox.Name);
                        writer.AddAttribute(HtmlTextWriterAttribute.Alt, textBox.Name);
                    }
                    else
                        writer.AddAttribute(HtmlTextWriterAttribute.Alt, textBox.Value as String);
                    if (useStyle && !String.IsNullOrEmpty(textBox.Style))
                        RenderStyle(ref writer, textBox.Style);
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, textBox.Value as String);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    if (!String.IsNullOrEmpty(textBox.Name))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Name, textBox.Name);
                        writer.Write(textBox.Name);
                    }
                    else
                        writer.Write(textBox.Value as String);
                    if (useStyle && !String.IsNullOrEmpty(textBox.Style))
                        RenderStyle(ref writer, textBox.Style);
                    writer.RenderEndTag();
                }
            else if (tag.HasValue)
            {
                if (!String.IsNullOrEmpty(textBox.Name))
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, textBox.Name);
                if (useStyle && !String.IsNullOrEmpty(textBox.Style))
                    RenderStyle(ref writer, textBox.Style);
                if (!String.IsNullOrEmpty(textBox.Tag))
                {
                    if (textBox.Tag.Equals("P", StringComparison.OrdinalIgnoreCase))
                        writer.RenderBeginTag(HtmlTextWriterTag.P);
                    else if (textBox.Tag.Equals("Span", StringComparison.OrdinalIgnoreCase))
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                }
                else
                    writer.RenderBeginTag(tag.Value);
                writer.Write(textBox.Value);
                writer.RenderEndTag();
            }
            else
            {
                var translationsKey = textBox.Value as String;
                Object translationsValue = null;
                if (translationsKey != null && translationsKey.StartsWith("@"))
                {
                    if (TranslationsDictionary != null && TranslationsDictionary.Count > 0 &&
                        TranslationsDictionary.ContainsKey(translationsKey.Substring(1)))
                        translationsValue = TranslationsDictionary[translationsKey.Substring(1)];
                }
                else if (translationsKey != null && translationsKey.Equals("=DATE"))
                    translationsValue = DateTime.Now;
                writer.Write(translationsValue ?? textBox.Value);
            }
        }

        public void RenderChart(ref HtmlTextWriter writer, Chart chart)
        {
            if (chart == null) return;
            string chartPath = chart.Render();
            if (chartPath == null) return;
            if (!String.IsNullOrEmpty(HtmlDirectoryPath) && Directory.Exists(HtmlDirectoryPath))
                writer.AddAttribute(HtmlTextWriterAttribute.Src,
                                    String.Format("..{0}{1}", Path.DirectorySeparatorChar, chartPath));
            else
                writer.AddAttribute(HtmlTextWriterAttribute.Src, chartPath);
            if (!String.IsNullOrEmpty(chart.Name))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Name, chart.Name);
                writer.AddAttribute(HtmlTextWriterAttribute.Alt, chart.Name);
            }
            else
                writer.AddAttribute(HtmlTextWriterAttribute.Alt, chartPath);
            if (!String.IsNullOrEmpty(chart.Style))
                RenderStyle(ref writer, chart.Style);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
        }

        public String RenderStyle(String style)
        {
            return style.StartsWith("#") || style.StartsWith(".") ? style : "." + style;
        }

        public void RenderStyle(ref HtmlTextWriter writer, String style)
        {
            if (style.StartsWith("#"))
                writer.AddAttribute(HtmlTextWriterAttribute.Id, style.Substring(1));
            else if (style.StartsWith("."))
                writer.AddAttribute(HtmlTextWriterAttribute.Class, style.Substring(1));
            else
                writer.AddAttribute(HtmlTextWriterAttribute.Class, style);
        }
    }
}