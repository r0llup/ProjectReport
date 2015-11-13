using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms.DataVisualization.Charting;
using ClassLibraryReport.Data;
using ClassLibraryReport.Interfaces;
using Series = ClassLibraryReport.Data.Series;

namespace ClassLibraryReport.View
{
    public class Chart : IChart
    {
        public Chart() : this(default(String))
        {
        }

        public Chart(String name) : this(name, null)
        {
        }

        public Chart(String name, String style) : this(name, style, null)
        {
        }

        public Chart(String name, String style, String tag) : this(name, style, tag, new Seriess())
        {
        }

        public Chart(String name, String style, String tag, Seriess seriess) :
            this(name, style, tag, seriess, null, null)
        {
        }

        public Chart(String name, String style, String tag, Seriess seriess, String imagesDirectoryPath) :
            this(name, style, tag, seriess, imagesDirectoryPath, null)
        {
        }

        public Chart(String name, String style, String tag, Seriess seriess, String imagesDirectoryPath,
                     DataSet dataSet) :
                         this(name, style, tag, seriess, imagesDirectoryPath, dataSet, null)
        {
        }

        public Chart(String name, String style, String tag, Seriess seriess, String imagesDirectoryPath,
                     DataSet dataSet, String xLabel) :
                         this(name, style, tag, seriess, imagesDirectoryPath,
                              new System.Windows.Forms.DataVisualization.Charting.Chart(), dataSet, xLabel, null)
        {
        }

        public Chart(String name, String style, String tag, Seriess seriess, String imagesDirectoryPath,
                     DataSet dataSet, String xLabel, String yLabel) :
                         this(name, style, tag, seriess, imagesDirectoryPath,
                              new System.Windows.Forms.DataVisualization.Charting.Chart(), dataSet, xLabel, yLabel)
        {
        }

        public Chart(String name, String style, String tag, Seriess seriess, String imagesDirectoryPath,
                     System.Windows.Forms.DataVisualization.Charting.Chart winFormsChart, DataSet dataSet,
                     String xLabel, String yLabel)
        {
            Name = name;
            Style = style;
            Tag = tag;
            Seriess = seriess;
            ImagesDirectoryPath = imagesDirectoryPath;
            WinFormsChart = winFormsChart;
            DataSet = dataSet;
            XLabel = xLabel;
            YLabel = yLabel;
        }

        public Chart(Chart chart)
        {
            Name = chart.Name;
            Style = chart.Style;
            Tag = chart.Tag;
            Seriess = new Seriess(chart.Seriess);
            ImagesDirectoryPath = chart.ImagesDirectoryPath;
            WinFormsChart = chart.WinFormsChart;
            DataSet = new DataSet(chart.DataSet);
            XLabel = chart.XLabel;
            YLabel = chart.YLabel;
        }

        public Chart(SerializationInfo si, StreamingContext sc)
        {
            Name = si.GetValue("Name", typeof (String)) as String;
            Style = si.GetValue("Style", typeof (String)) as String;
            Tag = si.GetValue("Tag", typeof (String)) as String;
            Seriess = si.GetValue("Seriess", typeof (Seriess)) as Seriess;
            ImagesDirectoryPath = si.GetValue("ImagesDirectoryPath", typeof (String)) as String;
            WinFormsChart = (System.Windows.Forms.DataVisualization.Charting.Chart)
                            si.GetValue("WinFormsChart", typeof (System.Windows.Forms.DataVisualization.Charting.Chart));
            DataSet = si.GetValue("DataSet", typeof (DataSet)) as DataSet;
            XLabel = si.GetValue("XLabel", typeof (String)) as String;
            YLabel = si.GetValue("YLabel", typeof (String)) as String;
        }

        public String Name { get; set; }
        public String Style { get; set; }
        public String Tag { get; set; }
        public Seriess Seriess { get; set; }
        public String ImagesDirectoryPath { get; set; }
        public System.Windows.Forms.DataVisualization.Charting.Chart WinFormsChart { get; set; }
        public DataSet DataSet { get; set; }
        public String XLabel { get; set; }
        public String YLabel { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Name", Name);
            si.AddValue("Style", Style);
            si.AddValue("Tag", Tag);
            si.AddValue("Seriess", Seriess);
            si.AddValue("ImagesDirectoryPath", ImagesDirectoryPath);
            si.AddValue("WinFormsChart", WinFormsChart);
            si.AddValue("DataSet", DataSet);
            si.AddValue("XLabel", XLabel);
            si.AddValue("YLabel", YLabel);
        }

        public virtual Int32 CompareTo(IDisplayable displayable)
        {
            return displayable is Chart
                       ? String.Compare(Name, displayable.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ Name: {0} ][ Style: {1} ][ Tag: {2} ][ Seriess: {3} ]" +
                                 "[ ImagesDirectoryPath: {4} ][ DataSet: {5} ][ XLabel: {6} ][ YLabel: {7} ]",
                                 Name, Style, Tag, Seriess, ImagesDirectoryPath, DataSet, XLabel, YLabel);
        }

        public String Render()
        {
            if (Seriess == null || Seriess.IsDataListEmpty() ||
                ImagesDirectoryPath == null || WinFormsChart == null) return null;
            var chartArea = new ChartArea();
            if (!String.IsNullOrEmpty(XLabel))
                chartArea.AxisX.Title = XLabel;
            if (!String.IsNullOrEmpty(YLabel))
                chartArea.AxisY.Title = YLabel;
            WinFormsChart.ChartAreas.Add(chartArea);
            foreach (Series series in Seriess.DataList)
            {
                var seriesType = SeriesChartType.FastLine;
                switch (series.Type)
                {
                    case "Bar":
                        seriesType = SeriesChartType.Bar;
                        break;
                    case "Column":
                        seriesType = SeriesChartType.Column;
                        break;
                    case "Line":
                        seriesType = SeriesChartType.Line;
                        break;
                    case "Pie":
                        seriesType = SeriesChartType.Pie;
                        break;
                    case "Point":
                        seriesType = SeriesChartType.Point;
                        break;
                }
                var seriesCharting = new System.Windows.Forms.DataVisualization.Charting.Series
                    {
                        Name = series.Name,
                        ChartType = seriesType
                    };
                WinFormsChart.Series.Add(seriesCharting);
                WinFormsChart.Series[series.Name].Points.DataBindXY(
                    series.DataList[0].DataList, series.DataList[1].DataList);
            }
            WinFormsChart.Invalidate();
            string chartPath = String.Format("{0}{1}{2}.png", ImagesDirectoryPath,
                                             Path.DirectorySeparatorChar, Seriess.Name);
            WinFormsChart.SaveImage(chartPath, ChartImageFormat.Png);
            return chartPath;
        }
    }
}