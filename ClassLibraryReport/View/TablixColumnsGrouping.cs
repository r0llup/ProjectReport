using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TablixColumnsGrouping : ISerializable
    {
        public TablixColumnsGrouping() : this(default(String))
        {
        }

        public TablixColumnsGrouping(String columnIndex) : this(columnIndex, null)
        {
        }

        public TablixColumnsGrouping(String columnIndex, String columnSortDirection) :
            this(columnIndex, columnSortDirection, null)
        {
        }

        public TablixColumnsGrouping(String columnIndex, String columnSortDirection,
                                     String orderByColumnIndex) :
                                         this(columnIndex, columnSortDirection, orderByColumnIndex, null)
        {
        }

        public TablixColumnsGrouping(String columnIndex, String columnSortDirection,
                                     String orderByColumnIndex, String _class) :
                                         this(columnIndex, columnSortDirection, orderByColumnIndex, _class, null)
        {
        }

        public TablixColumnsGrouping(String columnIndex, String columnSortDirection,
                                     String orderByColumnIndex, String _class, String groupBy) :
                                         this(
                                         columnIndex, columnSortDirection, orderByColumnIndex, _class, groupBy, null)
        {
        }

        public TablixColumnsGrouping(String columnIndex, String columnSortDirection,
                                     String orderByColumnIndex, String _class, String groupBy, String expandable) :
                                         this(columnIndex, columnSortDirection, orderByColumnIndex, _class, groupBy,
                                              expandable, null)
        {
        }

        public TablixColumnsGrouping(String columnIndex, String columnSortDirection,
                                     String orderByColumnIndex, String _class, String groupBy, String expandable,
                                     String expandSingle) :
                                         this(columnIndex, columnSortDirection, orderByColumnIndex, _class, groupBy,
                                              expandable, expandSingle, null)
        {
        }

        public TablixColumnsGrouping(String columnIndex, String columnSortDirection,
                                     String orderByColumnIndex, String _class, String groupBy, String expandable,
                                     String expandSingle, String dateFormat)
        {
            ColumnIndex = columnIndex;
            ColumnSortDirection = columnSortDirection;
            OrderByColumnIndex = orderByColumnIndex;
            Class = _class;
            GroupBy = groupBy;
            Expandable = expandable;
            ExpandSingle = expandSingle;
            DateFormat = dateFormat;
        }

        public TablixColumnsGrouping(TablixColumnsGrouping grouping)
        {
            ColumnIndex = grouping.ColumnIndex;
            ColumnSortDirection = grouping.ColumnSortDirection;
            OrderByColumnIndex = grouping.OrderByColumnIndex;
            Class = grouping.Class;
            GroupBy = grouping.GroupBy;
            Expandable = grouping.Expandable;
            ExpandSingle = grouping.ExpandSingle;
            DateFormat = grouping.DateFormat;
        }

        public TablixColumnsGrouping(SerializationInfo si, StreamingContext sc)
        {
            ColumnIndex = si.GetValue("ColumnIndex", typeof (String)) as String;
            ColumnSortDirection = si.GetValue("ColumnSortDirection", typeof (String)) as String;
            OrderByColumnIndex = si.GetValue("OrderByColumnIndex", typeof (String)) as String;
            Class = si.GetValue("Class", typeof (String)) as String;
            GroupBy = si.GetValue("GroupBy", typeof (String)) as String;
            Expandable = si.GetValue("Expandable", typeof (String)) as String;
            ExpandSingle = si.GetValue("ExpandSingle", typeof (String)) as String;
            DateFormat = si.GetValue("DateFormat", typeof (String)) as String;
        }

        public String ColumnIndex { get; set; }
        public String ColumnSortDirection { get; set; }
        public String OrderByColumnIndex { get; set; }
        public String Class { get; set; }
        public String GroupBy { get; set; }
        public String Expandable { get; set; }
        public String ExpandSingle { get; set; }
        public String DateFormat { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("ColumnIndex", ColumnIndex);
            si.AddValue("ColumnSortDirection", ColumnSortDirection);
            si.AddValue("OrderByColumnIndex", OrderByColumnIndex);
            si.AddValue("Class", Class);
            si.AddValue("GroupBy", GroupBy);
            si.AddValue("Expandable", Expandable);
            si.AddValue("ExpandSingle", ExpandSingle);
            si.AddValue("DateFormat", DateFormat);
        }

        public override String ToString()
        {
            return String.Format("[ Grouping ][ ColumnIndex: {0} ][ ColumnSortDirection: {1} ]" +
                                 "[ OrderByColumnIndex: {2} ][ Class: {3} ][ GroupBy: {4} ][ Expandable: {5} ]" +
                                 "[ ExpandSingle: {6} ][ DateFormat: {7} ]",
                                 ColumnIndex, ColumnSortDirection, OrderByColumnIndex, Class, GroupBy, Expandable,
                                 ExpandSingle, DateFormat);
        }
    }
}