using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class PageFooter : Container
    {
        public PageFooter()
        {
        }

        public PageFooter(String name) : base(name)
        {
        }

        public PageFooter(List<IDisplayable> displayableList) : base(displayableList)
        {
        }

        public PageFooter(String name, String style) : base(name, style)
        {
        }

        public PageFooter(String name, String style, String tag) : base(name, style, tag)
        {
        }

        public PageFooter(String name, String style, String tag, List<IDisplayable> displayableList) :
            base(name, style, tag, displayableList)
        {
        }

        public PageFooter(PageFooter pageFooter) : base(pageFooter)
        {
        }

        public PageFooter(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }

        public override Int32 CompareTo(IDisplayable displayable)
        {
            return displayable is PageFooter
                       ? String.Compare(Name, displayable.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ PageFooter ]{0}", base.ToString());
        }
    }
}