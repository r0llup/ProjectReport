using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Data;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class Report : IReport
    {
        public Report() : this(default(String))
        {
        }

        public Report(String name) : this(name, new DataSets())
        {
        }

        public Report(DataSets dataSets) : this(null, dataSets)
        {
        }

        public Report(String name, DataSets dataSets) :
            this(name, dataSets, new Body())
        {
        }

        public Report(String name, DataSets dataSets, Body body) :
            this(name, dataSets, body, new PageHeader())
        {
        }

        public Report(String name, DataSets dataSets, Body body,
                      PageHeader pageHeader) :
                          this(name, dataSets, body, pageHeader, new PageFooter())
        {
        }

        public Report(String name, DataSets dataSets, Body body,
                      PageHeader pageHeader, PageFooter pageFooter) :
                          this(name, dataSets, body, pageHeader, pageFooter, true)
        {
        }

        public Report(String name, DataSets dataSets, Body body,
                      PageHeader pageHeader, PageFooter pageFooter, Boolean displayTitle)
        {
            Name = name;
            DataSets = dataSets;
            Body = body;
            PageHeader = pageHeader;
            PageFooter = pageFooter;
            DisplayTitle = displayTitle;
        }

        public Report(Report report)
        {
            Name = report.Name;
            DataSets = new DataSets(report.DataSets);
            Body = new Body(report.Body);
            PageHeader = new PageHeader(report.PageHeader);
            PageFooter = new PageFooter(report.PageFooter);
            DisplayTitle = report.DisplayTitle;
        }

        public Report(SerializationInfo si, StreamingContext sc)
        {
            Name = si.GetValue("Name", typeof (String)) as String;
            DataSets = si.GetValue("DataSets", typeof (DataSets)) as DataSets;
            Body = si.GetValue("Body", typeof (Body)) as Body;
            PageHeader = si.GetValue("PageHeader", typeof (PageHeader)) as PageHeader;
            PageFooter = si.GetValue("PageFooter", typeof (PageFooter)) as PageFooter;
            DisplayTitle = (Boolean) si.GetValue("DisplayTitle", typeof (Boolean));
        }

        public String Name { get; set; }
        public DataSets DataSets { get; set; }
        public Body Body { get; set; }
        public PageHeader PageHeader { get; set; }
        public PageFooter PageFooter { get; set; }
        public Boolean DisplayTitle { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Name", Name);
            si.AddValue("DataSets", DataSets);
            si.AddValue("Body", Body);
            si.AddValue("PageHeader", PageHeader);
            si.AddValue("PageFooter", PageFooter);
            si.AddValue("DisplayTitle", DisplayTitle);
        }

        public Int32 CompareTo(IReport report)
        {
            return report is Report
                       ? String.Compare(Name, report.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ Report ][ Name: {0} ]{1}{2}{3}[ DisplayTitle: {4} ]",
                                 Name, Body, PageHeader, PageFooter, DisplayTitle);
        }
    }
}