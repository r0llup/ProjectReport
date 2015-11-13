using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TablixCell : ITablixCell
    {
        public TablixCell()
        {
        }

        public TablixCell(String id) : this(id, null)
        {
        }

        public TablixCell(String id, String rowSpan) : this(id, rowSpan, null)
        {
        }

        public TablixCell(String id, String rowSpan, String colSpan) :
            this(id, rowSpan, colSpan, false)
        {
        }

        public TablixCell(String id, String rowSpan, String colSpan, Boolean header) :
            this(id, rowSpan, colSpan, header, null)
        {
        }

        public TablixCell(String id, String rowSpan, String colSpan, Boolean header,
                          TextBox textBox) :
                              this(id, rowSpan, colSpan, header, textBox, null)
        {
        }

        public TablixCell(String id, String rowSpan, String colSpan, Boolean header,
                          TextBox textBox, String name) :
                              this(id, rowSpan, colSpan, header, textBox, name, null)
        {
        }

        public TablixCell(String id, String rowSpan, String colSpan, Boolean header,
                          TextBox textBox, String name, String style) :
                              this(id, rowSpan, colSpan, header, textBox, name, style, null)
        {
        }

        public TablixCell(String id, String rowSpan, String colSpan, Boolean header,
                          TextBox textBox, String name, String style, String tag)
        {
            Id = id;
            RowSpan = rowSpan;
            ColSpan = colSpan;
            Header = header;
            TextBox = textBox;
            Name = name;
            Style = style;
            Tag = tag;
        }

        public TablixCell(TablixCell tablixCell)
        {
            Id = tablixCell.Id;
            RowSpan = tablixCell.RowSpan;
            ColSpan = tablixCell.ColSpan;
            Header = tablixCell.Header;
            Name = tablixCell.Name;
            Style = tablixCell.Style;
            Tag = tablixCell.Tag;
        }

        public TablixCell(SerializationInfo si, StreamingContext sc)
        {
            Id = si.GetValue("Id", typeof (String)) as String;
            RowSpan = si.GetValue("RowSpan", typeof (String)) as String;
            ColSpan = si.GetValue("ColSpan", typeof (String)) as String;
            Header = (Boolean) si.GetValue("Header", typeof (Boolean));
            Name = si.GetValue("Name", typeof (String)) as String;
            Style = si.GetValue("Style", typeof (String)) as String;
            Tag = si.GetValue("Tag", typeof (String)) as String;
        }

        public String Id { get; set; }
        public String RowSpan { get; set; }
        public String ColSpan { get; set; }
        public Boolean Header { get; set; }
        public TextBox TextBox { get; set; }
        public String Name { get; set; }
        public String Style { get; set; }
        public String Tag { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Id", Id);
            si.AddValue("RowSpan", RowSpan);
            si.AddValue("ColSpan", ColSpan);
            si.AddValue("Header", Header);
            si.AddValue("Name", Header);
            si.AddValue("Style", Header);
            si.AddValue("Tag", Header);
        }

        public Int32 CompareTo(IDisplayable displayable)
        {
            return displayable is TablixCell
                       ? String.Compare(Name, displayable.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ TablixCell ][ Id: {0} ][ RowSpan: {1} ]" +
                                 "[ ColSpan: {2} ][ Header: {3} ][ Name: {4} ][ Style: {5} ]" +
                                 "[ Tag: {6} ]",
                                 Id, RowSpan, ColSpan, Header, Name, Style, Tag);
        }
    }
}