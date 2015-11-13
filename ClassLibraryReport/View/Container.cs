using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    [Serializable]
    public abstract class Container : Datas<IDisplayable>, IDisplayable
    {
        protected Container() : this(default(String))
        {
        }

        protected Container(String name) : this(name, null)
        {
        }

        protected Container(List<IDisplayable> displayableList) : this(null, null, null, displayableList)
        {
        }

        protected Container(String name, String style) : this(name, style, null)
        {
        }

        protected Container(String name, String style, String tag) :
            this(name, style, tag, new List<IDisplayable>())
        {
        }

        protected Container(String name, String style, String tag, List<IDisplayable> displayableList) :
            base(displayableList)
        {
            Name = name;
            Style = style;
            Tag = tag;
        }

        protected Container(Container container) : base(container)
        {
            Name = container.Name;
            Style = container.Style;
            Tag = container.Tag;
        }

        protected Container(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
            Name = si.GetValue("Name", typeof (String)) as String;
            Style = si.GetValue("Style", typeof (String)) as String;
            Tag = si.GetValue("Tag", typeof (String)) as String;
        }

        public String Name { get; set; }
        public String Style { get; set; }
        public String Tag { get; set; }

        public override void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Name", Name);
            si.AddValue("Style", Style);
            si.AddValue("Tag", Tag);
            base.GetObjectData(si, sc);
        }

        public virtual Int32 CompareTo(IDisplayable displayable)
        {
            return displayable is Container
                       ? String.Compare(Name, displayable.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ Name: {0} ][ Style: {1} ][ Tag: {2} ]",
                                 Name, Style, Tag);
        }
    }
}