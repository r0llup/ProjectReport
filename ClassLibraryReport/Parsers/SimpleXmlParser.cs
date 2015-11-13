using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using ClassLibraryReport.Data;
using ClassLibraryReport.Interfaces;
using ClassLibraryReport.View;

namespace ClassLibraryReport.Parsers
{
    public class SimpleXmlParser : IXmlParser
    {
        public SimpleXmlParser() : this(null)
        {
        }

        public SimpleXmlParser(String xmlFilePath) : this(xmlFilePath, null)
        {
        }

        public SimpleXmlParser(String xmlFilePath, String xsdFilePath) :
            this(xmlFilePath, xsdFilePath, default(Reports))
        {
        }

        public SimpleXmlParser(String xmlFilePath, String xsdFilePath, Report report) :
            this(xmlFilePath, xsdFilePath, new Reports(new List<Report> {report}))
        {
        }

        public SimpleXmlParser(String xmlFilePath, String xsdFilePath, Reports reports)
        {
            XmlFilePath = xmlFilePath;
            XsdFilePath = xsdFilePath;
            XmlReaderSettings = new XmlReaderSettings();
            XmlReaderSettings.Schemas.Add(null, XmlReader.Create(XsdFilePath));
            XmlReaderSettings.ValidationType = ValidationType.Schema;
            XmlReaderSettings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;
            XmlReaderSettings.ValidationEventHandler += settings_ValidationEventHandler;
            XmlReaderSettings.IgnoreWhitespace = true;
            XmlReaderSettings.IgnoreComments = true;
            XmlReader = XmlReader.Create(XmlFilePath, XmlReaderSettings);
            Reports = reports;
        }

        public String XmlFilePath { get; set; }
        public String XsdFilePath { get; set; }
        public XmlReaderSettings XmlReaderSettings { get; set; }
        public XmlReader XmlReader { get; set; }
        public Reports Reports { get; set; }

        public void Parse()
        {
            int cptReport = -1;
            int cptDataSet = -1;
            Body body = null;
            PageHeader pageHeader = null;
            PageFooter pageFooter = null;
            Box box = null;
            Tablix tablix = null;
            TablixBody tablixBody = null;
            TablixHeader tablixHeader = null;
            TablixFooter tablixFooter = null;
            TablixRow tablixRow = null;
            TablixCell tablixCell = null;
            TablixColumns tablixColumns = null;
            TablixColumnsGrouping tablixColumnsGrouping = null;
            TablixChart tablixChart = null;
            TablixColumnHierarchy tablixColumnHierarchy = null;
            Group group = null;
            TextBox textBox = null;
            Chart chart = null;
            bool inBody = false;
            bool inPageHeader = false;
            bool inPageFooter = false;
            bool inBox = false;
            bool inTablixBody = false;
            bool inTablixHeader = false;
            bool inTablixFooter = false;
            bool inTablixCell = false;
            bool inTablixColumnHierarchy = false;
            while (XmlReader.Read())
                switch (XmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (XmlReader.LocalName)
                        {
                            case "Report":
                                cptReport++;
                                if (XmlReader.HasAttributes)
                                {
                                    Reports.DataList[cptReport].Name = XmlReader.GetAttribute("Name");
                                    Boolean displayTitle;
                                    Boolean.TryParse(XmlReader.GetAttribute("DisplayTitle"), out displayTitle);
                                    Reports.DataList[cptReport].DisplayTitle = displayTitle;
                                }
                                break;
                            case "DataSets":
                                cptDataSet = -1;
                                break;
                            case "DataSet":
                                cptDataSet++;
                                if (XmlReader.HasAttributes)
                                    Reports.DataList[cptReport].DataSets.DataList[cptDataSet].Name =
                                        XmlReader.GetAttribute("Name");
                                break;
                            case "Field":
                                if (XmlReader.HasAttributes)
                                    Reports.DataList[cptReport].DataSets.DataList[cptDataSet].
                                        FieldDescriptors.AddData(new FieldDescriptor(
                                                                     XmlReader.GetAttribute("Id"),
                                                                     XmlReader.GetAttribute("Type")));
                                break;
                            case "Body":
                                inBody = true;
                                inPageHeader = false;
                                inPageFooter = false;
                                inBox = false;
                                body = new Body();
                                if (XmlReader.HasAttributes)
                                {
                                    body.Name = XmlReader.GetAttribute("Name");
                                    body.Style = XmlReader.GetAttribute("Style");
                                    body.Tag = XmlReader.GetAttribute("Tag");
                                }
                                break;
                            case "PageHeader":
                                inBody = false;
                                inPageHeader = true;
                                inPageFooter = false;
                                inBox = false;
                                pageHeader = new PageHeader();
                                if (XmlReader.HasAttributes)
                                {
                                    pageHeader.Name = XmlReader.GetAttribute("Name");
                                    pageHeader.Style = XmlReader.GetAttribute("Style");
                                    pageHeader.Tag = XmlReader.GetAttribute("Tag");
                                }
                                break;
                            case "PageFooter":
                                inBody = false;
                                inPageHeader = false;
                                inPageFooter = true;
                                inBox = false;
                                pageFooter = new PageFooter();
                                if (XmlReader.HasAttributes)
                                {
                                    pageFooter.Name = XmlReader.GetAttribute("Name");
                                    pageFooter.Style = XmlReader.GetAttribute("Style");
                                    pageFooter.Tag = XmlReader.GetAttribute("Tag");
                                }
                                break;
                            case "Box":
                                inBox = true;
                                box = new Box();
                                if (XmlReader.HasAttributes)
                                {
                                    box.Name = XmlReader.GetAttribute("Name");
                                    box.Style = XmlReader.GetAttribute("Style");
                                    box.Tag = XmlReader.GetAttribute("Tag");
                                }
                                break;
                            case "Tablix":
                                inTablixBody = false;
                                inTablixHeader = false;
                                inTablixFooter = false;
                                inTablixColumnHierarchy = false;
                                if (XmlReader.HasAttributes)
                                {
                                    Boolean dynamic;
                                    Boolean.TryParse(XmlReader.GetAttribute("Dynamic"), out dynamic);
                                    Boolean paginate;
                                    Boolean.TryParse(XmlReader.GetAttribute("Paginate"), out paginate);
                                    tablix = new Tablix(XmlReader.GetAttribute("Name"),
                                                        XmlReader.GetAttribute("Caption"),
                                                        XmlReader.GetAttribute("Style"),
                                                        dynamic, paginate, XmlReader.GetAttribute("Tag"),
                                                        XmlReader.GetAttribute("PaginationType"));
                                    foreach (DataSet t in Reports.DataList[cptReport].DataSets.DataList.Where(
                                        t => t.Name.Equals(XmlReader.GetAttribute("DataSet"))))
                                        tablix.DataSet = t;
                                }
                                break;
                            case "TablixBody":
                                inTablixBody = true;
                                inTablixHeader = false;
                                inTablixFooter = false;
                                tablixBody = new TablixBody();
                                if (XmlReader.HasAttributes)
                                {
                                    Boolean linked;
                                    Boolean.TryParse(XmlReader.GetAttribute("Linked"), out linked);
                                    tablixBody.Linked = linked;
                                }
                                break;
                            case "TablixHeader":
                                inTablixBody = false;
                                inTablixHeader = true;
                                inTablixFooter = false;
                                tablixHeader = new TablixHeader();
                                if (XmlReader.HasAttributes)
                                {
                                    Boolean linked;
                                    Boolean.TryParse(XmlReader.GetAttribute("Linked"), out linked);
                                    tablixHeader.Linked = linked;
                                }
                                break;
                            case "TablixFooter":
                                inTablixBody = false;
                                inTablixHeader = false;
                                inTablixFooter = true;
                                tablixFooter = new TablixFooter();
                                if (XmlReader.HasAttributes)
                                {
                                    Boolean linked;
                                    Boolean.TryParse(XmlReader.GetAttribute("Linked"), out linked);
                                    tablixFooter.Linked = linked;
                                }
                                break;
                            case "TablixRow":
                                tablixRow = new TablixRow();
                                if (XmlReader.HasAttributes)
                                {
                                    tablixRow.Name = XmlReader.GetAttribute("Name");
                                    tablixRow.Style = XmlReader.GetAttribute("Style");
                                    tablixRow.Tag = XmlReader.GetAttribute("Tag");
                                }
                                break;
                            case "TablixCell":
                                inTablixCell = true;
                                tablixCell = new TablixCell();
                                if (XmlReader.HasAttributes)
                                {
                                    Boolean header;
                                    Boolean.TryParse(XmlReader.GetAttribute("Header"), out header);
                                    tablixCell.Header = header;
                                    tablixCell.Id = XmlReader.GetAttribute("Id");
                                    tablixCell.RowSpan = XmlReader.GetAttribute("RowSpan");
                                    tablixCell.ColSpan = XmlReader.GetAttribute("ColSpan");
                                    tablixCell.Name = XmlReader.GetAttribute("Name");
                                    tablixCell.Style = XmlReader.GetAttribute("Style");
                                    tablixCell.Tag = XmlReader.GetAttribute("Tag");
                                }
                                break;
                            case "TablixColumns":
                                if (XmlReader.HasAttributes)
                                {
                                    Boolean reorderable;
                                    Boolean.TryParse(XmlReader.GetAttribute("Reorderable"), out reorderable);
                                    Boolean hideable;
                                    Boolean.TryParse(XmlReader.GetAttribute("Hideable"), out hideable);
                                    tablixColumns = new TablixColumns(XmlReader.GetAttribute("Sorting"),
                                                                      reorderable, hideable);
                                }
                                break;
                            case "TablixColumnsGrouping":
                                if (XmlReader.HasAttributes)
                                    tablixColumnsGrouping = new TablixColumnsGrouping(
                                        XmlReader.GetAttribute("ColumnIndex"),
                                        XmlReader.GetAttribute("ColumnSortDirection"),
                                        XmlReader.GetAttribute("OrderByColumnIndex"),
                                        XmlReader.GetAttribute("Class"),
                                        XmlReader.GetAttribute("GroupBy"),
                                        XmlReader.GetAttribute("Expandable"),
                                        XmlReader.GetAttribute("ExpandSingle"),
                                        XmlReader.GetAttribute("DateFormat"));
                                break;
                            case "TablixChart":
                                if (XmlReader.HasAttributes)
                                    tablixChart = new TablixChart(XmlReader.GetAttribute("Type"),
                                                                  XmlReader.GetAttribute("Subtitle"),
                                                                  XmlReader.GetAttribute("Inverted"),
                                                                  XmlReader.GetAttribute("Height"),
                                                                  XmlReader.GetAttribute("Container"),
                                                                  XmlReader.GetAttribute("FillOpacity"),
                                                                  XmlReader.GetAttribute("LegendDisabled"),
                                                                  XmlReader.GetAttribute("LegendLayout"),
                                                                  XmlReader.GetAttribute("LegendWidth"),
                                                                  XmlReader.GetAttribute("LegendX"),
                                                                  XmlReader.GetAttribute("LegendY"));
                                break;
                            case "TablixColumnHierarchy":
                                inTablixColumnHierarchy = true;
                                tablixColumnHierarchy = new TablixColumnHierarchy();
                                break;
                            case "Group":
                                if (XmlReader.HasAttributes)
                                {
                                    group = new Group(XmlReader.GetAttribute("Name"), XmlReader.GetAttribute("On"));
                                    var dataSet = new DataSet
                                        {
                                            FieldDescriptors = tablix.DataSet.FieldDescriptors,
                                            Name = tablix.DataSet.Name,
                                            Stats = tablix.DataSet.Stats
                                        };
                                    int groupColumnIndex = dataSet.FieldDescriptors.ContainsDataById(group.On);
                                    if (groupColumnIndex == -1) continue;
                                    var columnValues = new List<Object>();
                                    foreach (Fields fields in tablix.DataSet.Fieldss.DataList.Where(
                                        fields => fields != null && !fields.IsDataListEmpty() &&
                                                  !columnValues.Contains(fields.DataList[groupColumnIndex].Value)))
                                        columnValues.Add(fields.DataList[groupColumnIndex].Value);
                                    foreach (object columnValue in columnValues)
                                    {
                                        var groupStats = new Stats();
                                        var sums = new List<Double>();
                                        int counter = 0;
                                        for (int index = 0; index < dataSet.FieldDescriptors.DataList.Count(); index++)
                                            sums.Add(0d);
                                        foreach (Fields fields in tablix.DataSet.Fieldss.DataList)
                                        {
                                            if (fields.DataList[groupColumnIndex].Value != columnValue) continue;
                                            var fieldsCopy = new Fields();
                                            int index = 0;
                                            foreach (Field field in fields.DataList)
                                            {
                                                fieldsCopy.AddData(new Field(field));
                                                if (field.Value is Decimal)
                                                    sums[index] += Decimal.ToDouble((Decimal) field.Value);
                                                else if (field.Value is Int32)
                                                    sums[index] += (Int32) field.Value;
                                                index++;
                                            }
                                            dataSet.Fieldss.AddData(fieldsCopy);
                                            counter++;
                                        }
                                        foreach (double sum in sums)
                                        {
                                            groupStats.Sums.AddData(sum);
                                            groupStats.Avgs.AddData(sum/counter);
                                        }
                                        dataSet.Statss.DataList.Add(groupStats);
                                        dataSet.Statss.GroupList.Add(columnValue);
                                        var endGroupFields = new Fields();
                                        for (int index = 0; index < dataSet.FieldDescriptors.DataList.Count(); index++)
                                            endGroupFields.AddData(new Field("_END_GROUP_"));
                                        dataSet.Fieldss.AddData(endGroupFields);
                                    }
                                    tablix.DataSet = dataSet;
                                }
                                break;
                            case "TextBox":
                                if (XmlReader.HasAttributes)
                                    textBox = new TextBox(XmlReader.GetAttribute("Name"),
                                                          XmlReader.GetAttribute("Value"),
                                                          XmlReader.GetAttribute("Style"),
                                                          XmlReader.GetAttribute("Tag"), XmlReader.GetAttribute("Type"));
                                break;
                            case "Chart":
                                if (XmlReader.HasAttributes)
                                {
                                    chart = new Chart(XmlReader.GetAttribute("Name"),
                                                      XmlReader.GetAttribute("Style"), XmlReader.GetAttribute("Tag"),
                                                      null, XmlReader.GetAttribute("ImagesDirectoryPath"), null,
                                                      XmlReader.GetAttribute("XLabel"), XmlReader.GetAttribute("YLabel"));
                                    foreach (DataSet t in Reports.DataList[cptReport].DataSets.DataList.Where(
                                        t => t.Name.Equals(XmlReader.GetAttribute("DataSet"))))
                                        chart.DataSet = t;
                                    string xSerieColumnName = XmlReader.GetAttribute("XSerieColumnName");
                                    int xSerieColumnIndex =
                                        chart.DataSet.FieldDescriptors.ContainsDataById(xSerieColumnName);
                                    string ySerieColumnName = XmlReader.GetAttribute("YSerieColumnName");
                                    int ySerieColumnIndex =
                                        chart.DataSet.FieldDescriptors.ContainsDataById(ySerieColumnName);
                                    if (xSerieColumnIndex > -1 && ySerieColumnIndex > -1)
                                    {
                                        var seriess = new Seriess
                                            {
                                                Name =
                                                    String.Format("Seriess{0}{1}", xSerieColumnIndex, ySerieColumnIndex)
                                            };
                                        var series = new Series
                                            {
                                                Name =
                                                    String.Format("Series{0}{1}", xSerieColumnIndex, ySerieColumnIndex),
                                                Type = XmlReader.GetAttribute("Type")
                                            };
                                        var xSerie = new Serie();
                                        var ySerie = new Serie();
                                        foreach (Fields fields in chart.DataSet.Fieldss.DataList)
                                        {
                                            xSerie.DataList.Add(fields.DataList[xSerieColumnIndex].Value);
                                            ySerie.DataList.Add(fields.DataList[ySerieColumnIndex].Value);
                                        }
                                        series.DataList.Add(xSerie);
                                        series.DataList.Add(ySerie);
                                        seriess.AddData(series);
                                        chart.Seriess = seriess;
                                    }
                                }
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        switch (XmlReader.LocalName)
                        {
                            case "Body":
                                Reports.DataList[cptReport].Body = body;
                                break;
                            case "PageHeader":
                                Reports.DataList[cptReport].PageHeader = pageHeader;
                                break;
                            case "PageFooter":
                                Reports.DataList[cptReport].PageFooter = pageFooter;
                                break;
                            case "Box":
                                if (inBody)
                                    body.AddData(box);
                                else if (inPageHeader)
                                    pageHeader.AddData(box);
                                else if (inPageFooter)
                                    pageFooter.AddData(box);
                                inBox = false;
                                break;
                            case "Tablix":
                                if (inBox)
                                    box.AddData(tablix);
                                else if (inBody)
                                    body.AddData(tablix);
                                else if (inPageHeader)
                                    pageHeader.AddData(tablix);
                                else if (inPageFooter)
                                    pageFooter.AddData(tablix);
                                break;
                            case "TablixBody":
                                tablix.TablixBody = tablixBody;
                                inTablixBody = false;
                                break;
                            case "TablixHeader":
                                if (inTablixColumnHierarchy)
                                    tablixColumnHierarchy.TablixHeader = tablixHeader;
                                else
                                    tablix.TablixHeader = tablixHeader;
                                inTablixHeader = false;
                                break;
                            case "TablixFooter":
                                if (inTablixColumnHierarchy)
                                    tablixColumnHierarchy.TablixFooter = tablixFooter;
                                else
                                    tablix.TablixFooter = tablixFooter;
                                inTablixFooter = false;
                                break;
                            case "TablixRow":
                                if (inTablixBody)
                                    tablixBody.TablixRows.AddData(tablixRow);
                                else if (inTablixHeader)
                                    tablixHeader.TablixRows.AddData(tablixRow);
                                else if (inTablixFooter)
                                    tablixFooter.TablixRows.AddData(tablixRow);
                                break;
                            case "TablixCell":
                                tablixRow.TablixCells.AddData(tablixCell);
                                inTablixCell = false;
                                break;
                            case "TablixColumns":
                                tablix.TablixColumns = tablixColumns;
                                break;
                            case "TablixColumnsGrouping":
                                tablixColumns.TablixColumnsGrouping = tablixColumnsGrouping;
                                break;
                            case "TablixChart":
                                tablix.TablixChart = tablixChart;
                                break;
                            case "TablixColumnHierarchy":
                                tablix.TablixColumnHierarchy = tablixColumnHierarchy;
                                break;
                            case "Group":
                                tablixColumnHierarchy.Group = group;
                                break;
                            case "TextBox":
                                if (inTablixCell)
                                    tablixCell.TextBox = textBox;
                                else if (inBox)
                                    box.AddData(textBox);
                                else if (inBody)
                                    body.AddData(textBox);
                                else if (inPageHeader)
                                    pageHeader.AddData(textBox);
                                else if (inPageFooter)
                                    pageFooter.AddData(textBox);
                                break;
                            case "Chart":
                                if (inBox)
                                    box.AddData(chart);
                                else if (inBody)
                                    body.AddData(chart);
                                else if (inPageHeader)
                                    pageHeader.AddData(chart);
                                else if (inPageFooter)
                                    pageFooter.AddData(chart);
                                break;
                        }
                        break;
                }
        }

        private static void settings_ValidationEventHandler(Object sender, ValidationEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}