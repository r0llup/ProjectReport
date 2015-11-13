using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.View
{
    public class Group : IGroup
    {
        public Group()
        {
        }

        public Group(String name) : this(name, null)
        {
        }

        public Group(String name, String on)
        {
            Name = name;
            On = on;
        }

        public Group(Group group)
        {
            Name = group.Name;
            On = group.On;
        }

        public Group(SerializationInfo si, StreamingContext sc)
        {
            Name = si.GetValue("Name", typeof (String)) as String;
            On = si.GetValue("On", typeof (String)) as String;
        }

        public String Name { get; set; }
        public String On { get; set; }

        public Int32 CompareTo(IGroup group)
        {
            return group is Group
                       ? String.Compare(Name, group.Name, StringComparison.Ordinal)
                       : -1;
        }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Name", Name);
            si.AddValue("On", On);
        }

        public override String ToString()
        {
            return String.Format("[ Group ][ Name: {0} ][ On: {1} ]",
                                 Name, On);
        }
    }
}