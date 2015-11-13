using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Data;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class Tablix : ITablix
    {
        public Tablix() : this(default(String))
        {
        }

        public Tablix(String name) : this(name, null)
        {
        }

        public Tablix(String name, DataSet dataSet) :
            this(name, dataSet, null)
        {
        }

        public Tablix(String name, String caption, String style, Boolean dynamic,
                      Boolean paginate, String tag, String paginationType) :
                          this(name, new DataSet(), caption, new TablixBody(), new TablixHeader(),
                               new TablixFooter(), style, dynamic, paginate, new TablixColumns(),
                               null, tag, paginationType)
        {
        }

        public Tablix(String name, DataSet dataSet, String caption) :
            this(name, dataSet, caption, new TablixBody())
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody) :
            this(name, dataSet, caption, tablixBody, new TablixHeader())
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody,
                      TablixHeader tablixHeader) :
                          this(name, dataSet, caption, tablixBody, tablixHeader, null)
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody,
                      TablixHeader tablixHeader, TablixFooter tablixFooter) :
                          this(name, dataSet, caption, tablixBody, tablixHeader, tablixFooter, null)
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody,
                      TablixHeader tablixHeader, TablixFooter tablixFooter, String style) :
                          this(name, dataSet, caption, tablixBody, tablixHeader, tablixFooter, style, true)
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody,
                      TablixHeader tablixHeader, TablixFooter tablixFooter, String style, Boolean dynamic) :
                          this(name, dataSet, caption, tablixBody, tablixHeader, tablixFooter, style, dynamic, true)
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody,
                      TablixHeader tablixHeader, TablixFooter tablixFooter, String style,
                      Boolean dynamic, Boolean paginate) :
                          this(name, dataSet, caption, tablixBody, tablixHeader, tablixFooter, style,
                               dynamic, paginate, new TablixColumns())
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody,
                      TablixHeader tablixHeader, TablixFooter tablixFooter, String style,
                      Boolean dynamic, Boolean paginate, TablixColumns tablixColumns) :
                          this(name, dataSet, caption, tablixBody, tablixHeader, tablixFooter, style,
                               dynamic, paginate, tablixColumns, null)
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody,
                      TablixHeader tablixHeader, TablixFooter tablixFooter, String style,
                      Boolean dynamic, Boolean paginate, TablixColumns tablixColumns,
                      TablixChart tablixChart) :
                          this(name, dataSet, caption, tablixBody, tablixHeader, tablixFooter, style,
                               dynamic, paginate, tablixColumns, tablixChart, null)
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody,
                      TablixHeader tablixHeader, TablixFooter tablixFooter, String style,
                      Boolean dynamic, Boolean paginate, TablixColumns tablixColumns,
                      TablixChart tablixChart, String tag) :
                          this(name, dataSet, caption, tablixBody, tablixHeader, tablixFooter, style,
                               dynamic, paginate, tablixColumns, tablixChart, tag, null)
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody,
                      TablixHeader tablixHeader, TablixFooter tablixFooter, String style,
                      Boolean dynamic, Boolean paginate, TablixColumns tablixColumns,
                      TablixChart tablixChart, String tag, String paginationType) :
                          this(name, dataSet, caption, tablixBody, tablixHeader, tablixFooter, style,
                               dynamic, paginate, tablixColumns, tablixChart, tag, paginationType, null)
        {
        }

        public Tablix(String name, DataSet dataSet, String caption, TablixBody tablixBody,
                      TablixHeader tablixHeader, TablixFooter tablixFooter, String style,
                      Boolean dynamic, Boolean paginate, TablixColumns tablixColumns,
                      TablixChart tablixChart, String tag, String paginationType,
                      TablixColumnHierarchy tablixColumnHierarchy)
        {
            Name = name;
            DataSet = dataSet;
            Caption = caption;
            TablixBody = tablixBody;
            TablixHeader = tablixHeader;
            TablixFooter = tablixFooter;
            Style = style;
            Dynamic = dynamic;
            Paginate = paginate;
            TablixColumns = tablixColumns;
            TablixChart = tablixChart;
            Tag = tag;
            PaginationType = paginationType;
            TablixColumnHierarchy = tablixColumnHierarchy;
        }

        public Tablix(Tablix tablix)
        {
            Name = tablix.Name;
            DataSet = new DataSet(tablix.DataSet);
            Caption = tablix.Caption;
            TablixBody = new TablixBody(tablix.TablixBody);
            TablixHeader = new TablixHeader(tablix.TablixHeader);
            TablixFooter = new TablixFooter(tablix.TablixFooter);
            Style = tablix.Style;
            Dynamic = tablix.Dynamic;
            Paginate = tablix.Paginate;
            TablixColumns = new TablixColumns(tablix.TablixColumns);
            TablixChart = new TablixChart(tablix.TablixChart);
            Tag = tablix.Tag;
            PaginationType = tablix.PaginationType;
            TablixColumnHierarchy = tablix.TablixColumnHierarchy;
        }

        public Tablix(SerializationInfo si, StreamingContext sc)
        {
            Name = si.GetValue("Name", typeof (String)) as String;
            DataSet = si.GetValue("DataSet", typeof (DataSet)) as DataSet;
            Caption = si.GetValue("Caption", typeof (String)) as String;
            TablixBody = si.GetValue("TablixBody", typeof (TablixBody)) as TablixBody;
            TablixHeader = si.GetValue("TablixHeader", typeof (TablixHeader)) as TablixHeader;
            TablixFooter = si.GetValue("TablixFooter", typeof (TablixFooter)) as TablixFooter;
            Style = si.GetValue("Style", typeof (String)) as String;
            Dynamic = (Boolean) si.GetValue("Dynamic", typeof (Boolean));
            Paginate = (Boolean) si.GetValue("Paginate", typeof (Boolean));
            TablixColumns = si.GetValue("TablixColumns", typeof (TablixColumns)) as TablixColumns;
            TablixChart = si.GetValue("TablixChart", typeof (TablixChart)) as TablixChart;
            Tag = si.GetValue("Tag", typeof (String)) as String;
            PaginationType = si.GetValue("PaginationType", typeof (String)) as String;
            TablixColumnHierarchy =
                si.GetValue("TablixColumnHierarchy", typeof (TablixColumnHierarchy)) as TablixColumnHierarchy;
        }

        public String Name { get; set; }
        public DataSet DataSet { get; set; }
        public String Caption { get; set; }
        public TablixBody TablixBody { get; set; }
        public TablixHeader TablixHeader { get; set; }
        public TablixFooter TablixFooter { get; set; }
        public String Style { get; set; }
        public Boolean Dynamic { get; set; }
        public Boolean Paginate { get; set; }
        public TablixColumns TablixColumns { get; set; }
        public TablixChart TablixChart { get; set; }
        public String Tag { get; set; }
        public String PaginationType { get; set; }
        public TablixColumnHierarchy TablixColumnHierarchy { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Name", Name);
            si.AddValue("DataSet", DataSet);
            si.AddValue("Caption", Caption);
            si.AddValue("TablixBody", TablixBody);
            si.AddValue("TablixHeader", TablixHeader);
            si.AddValue("TablixFooter", TablixFooter);
            si.AddValue("Style", Style);
            si.AddValue("Dynamic", Dynamic);
            si.AddValue("Paginate", Paginate);
            si.AddValue("TablixColumns", TablixColumns);
            si.AddValue("TablixChart", TablixChart);
            si.AddValue("Tag", Tag);
            si.AddValue("PaginationType", PaginationType);
            si.AddValue("TablixColumnHierarchy", TablixColumnHierarchy);
        }

        public Int32 CompareTo(IDisplayable displayable)
        {
            return displayable is Tablix
                       ? String.Compare(Name, displayable.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ Tablix ][ Name: {0} ]{1}[ Caption: {2} ][ Style: {3} ]" +
                                 "[ Dynamic: {4} ][ Paginate: {5} ]{6}{7}[ Tag: {8} ][ PaginationType: {9} ]" +
                                 "[ TablixColumnHierarchy: {10} ]",
                                 Name, DataSet, Caption, Style, Dynamic, Paginate, TablixColumns, TablixChart, Tag,
                                 PaginationType,
                                 TablixColumnHierarchy);
        }
    }
}