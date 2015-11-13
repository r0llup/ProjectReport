using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class Box : Container
    {
        public Box()
        {
        }

        public Box(String name) : base(name)
        {
        }

        public Box(List<IDisplayable> displayableList) : base(displayableList)
        {
        }

        public Box(String name, String style) : base(name, style)
        {
        }

        public Box(String name, String style, String tag) : base(name, style, tag)
        {
        }

        public Box(String name, String style, String tag, List<IDisplayable> displayableList) :
            base(name, style, tag, displayableList)
        {
        }

        public Box(Box box) : base(box)
        {
        }

        public Box(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }

        public override Int32 CompareTo(IDisplayable displayable)
        {
            return displayable is Box
                       ? String.Compare(Name, displayable.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ Box ]{0}", base.ToString());
        }
    }
}