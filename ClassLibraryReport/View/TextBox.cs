using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TextBox : ITextBox
    {
        public TextBox() : this(default(String))
        {
        }

        public TextBox(String name) : this(name, null)
        {
        }

        public TextBox(String name, Object value) : this(name, value, null)
        {
        }

        public TextBox(String name, Object value, String style) :
            this(name, value, style, null)
        {
        }

        public TextBox(String name, Object value, String style, String tag) :
            this(name, value, style, tag, null)
        {
        }

        public TextBox(String name, Object value, String style, String tag, String type)
        {
            Name = name;
            Value = value;
            Style = style;
            Tag = tag;
            Type = type;
        }

        public TextBox(TextBox textBox)
        {
            Name = textBox.Name;
            Value = textBox.Value;
            Style = textBox.Style;
            Tag = textBox.Tag;
            Type = textBox.Type;
        }

        public TextBox(SerializationInfo si, StreamingContext sc)
        {
            Name = si.GetValue("Name", typeof (String)) as String;
            Value = si.GetValue("Value", typeof (String)) as String;
            Style = si.GetValue("Style", typeof (String)) as String;
            Tag = si.GetValue("Tag", typeof (String)) as String;
            Type = si.GetValue("Type", typeof (String)) as String;
        }

        public String Type { get; set; }
        public String Name { get; set; }
        public Object Value { get; set; }
        public String Style { get; set; }
        public String Tag { get; set; }

        public virtual void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Name", Name);
            si.AddValue("Value", Value);
            si.AddValue("Style", Style);
            si.AddValue("Tag", Tag);
            si.AddValue("Type", Type);
        }

        public virtual Int32 CompareTo(IDisplayable displayable)
        {
            return displayable is TextBox
                       ? String.Compare(Name, displayable.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ Name: {0} ][ Value: {1} ][ Style: {2} ]" +
                                 "[ Tag: {3} ][ Type: {4} ]",
                                 Name, Value, Style, Tag, Type);
        }
    }
}