using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class Body : Container
    {
        public Body()
        {
        }

        public Body(String name) : base(name)
        {
        }

        public Body(List<IDisplayable> displayableList) : base(displayableList)
        {
        }

        public Body(String name, String style) : base(name, style)
        {
        }

        public Body(String name, String style, String tag) : base(name, style, tag)
        {
        }

        public Body(String name, String style, String tag, List<IDisplayable> displayableList) :
            base(name, style, tag, displayableList)
        {
        }

        public Body(Body body) : base(body)
        {
        }

        public Body(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }

        public override Int32 CompareTo(IDisplayable displayable)
        {
            return displayable is Body
                       ? String.Compare(Name, displayable.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ Body ]{0}", base.ToString());
        }
    }
}