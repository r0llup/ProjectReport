using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TablixRow : ITablixRow
    {
        public TablixRow() : this(new TablixCells())
        {
        }

        public TablixRow(TablixCells tablixCells) : this(tablixCells, null)
        {
        }

        public TablixRow(TablixCells tablixCells, String name) :
            this(tablixCells, name, null)
        {
        }

        public TablixRow(TablixCells tablixCells, String name,
                         String style) :
                             this(tablixCells, name, style, null)
        {
        }

        public TablixRow(TablixCells tablixCells, String name,
                         String style, String tag)
        {
            TablixCells = tablixCells;
            Name = name;
            Style = style;
            Tag = tag;
        }

        public TablixRow(TablixRow tablixRow)
        {
            TablixCells = new TablixCells(tablixRow.TablixCells);
            Name = tablixRow.Name;
            Style = tablixRow.Style;
            Tag = tablixRow.Tag;
        }

        public TablixRow(SerializationInfo si, StreamingContext sc)
        {
            TablixCells = si.GetValue("TablixCells", typeof (TablixCells)) as TablixCells;
            Name = si.GetValue("Name", typeof (String)) as String;
            Style = si.GetValue("Style", typeof (String)) as String;
            Tag = si.GetValue("Tag", typeof (String)) as String;
        }

        public TablixCells TablixCells { get; set; }
        public String Name { get; set; }
        public String Style { get; set; }
        public String Tag { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("TablixCells", TablixCells);
            si.AddValue("Name", Name);
            si.AddValue("Style", Style);
            si.AddValue("Tag", Tag);
        }

        public Int32 CompareTo(IDisplayable displayable)
        {
            return displayable is TablixRow
                       ? String.Compare(Name, displayable.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ TablixRow ][ Name: {0} ]" +
                                 "[ Style: {1} ][ Tag: {2} ]",
                                 Name, Style, Tag);
        }
    }
}