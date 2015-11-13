using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class PageHeader : Container
    {
        public PageHeader()
        {
        }

        public PageHeader(String name) : base(name)
        {
        }

        public PageHeader(List<IDisplayable> displayableList) : base(displayableList)
        {
        }

        public PageHeader(String name, String style) : base(name, style)
        {
        }

        public PageHeader(String name, String style, String tag) : base(name, style, tag)
        {
        }

        public PageHeader(String name, String style, String tag, List<IDisplayable> displayableList) :
            base(name, style, tag, displayableList)
        {
        }

        public PageHeader(PageHeader pageHeader) : base(pageHeader)
        {
        }

        public PageHeader(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }

        public override Int32 CompareTo(IDisplayable displayable)
        {
            return displayable is PageHeader
                       ? String.Compare(Name, displayable.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ PageHeader ]{0}", base.ToString());
        }
    }
}