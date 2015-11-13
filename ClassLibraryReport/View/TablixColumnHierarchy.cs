using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    public class TablixColumnHierarchy : ITablixColumnHierarchy
    {
        public TablixColumnHierarchy()
        {
        }

        public TablixColumnHierarchy(Group group) : this(group, null)
        {
        }

        public TablixColumnHierarchy(Group group, TablixHeader tablixHeader) :
            this(group, tablixHeader, null)
        {
        }

        public TablixColumnHierarchy(Group group, TablixHeader tablixHeader,
                                     TablixFooter tablixFooter)
        {
            Group = group;
            TablixHeader = tablixHeader;
            TablixFooter = tablixFooter;
        }

        public TablixColumnHierarchy(TablixColumnHierarchy tablixColumnHierarchy)
        {
            Group = tablixColumnHierarchy.Group;
            TablixHeader = tablixColumnHierarchy.TablixHeader;
            TablixFooter = tablixColumnHierarchy.TablixFooter;
        }

        public TablixColumnHierarchy(SerializationInfo si, StreamingContext sc)
        {
            Group = si.GetValue("Group", typeof (Group)) as Group;
            TablixHeader = si.GetValue("TablixHeader", typeof (TablixHeader)) as TablixHeader;
            TablixFooter = si.GetValue("TablixFooter", typeof (TablixFooter)) as TablixFooter;
        }

        public Group Group { get; set; }
        public TablixHeader TablixHeader { get; set; }
        public TablixFooter TablixFooter { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Group", Group);
            si.AddValue("TablixHeader", TablixHeader);
            si.AddValue("TablixFooter", TablixFooter);
        }

        public override String ToString()
        {
            return String.Format("[ TablixColumnHierarchy ][ Group: {0} ][ TablixHeader: {1} ]" +
                                 "[ TablixFooter: {2} ]",
                                 Group, TablixHeader, TablixFooter);
        }
    }
}