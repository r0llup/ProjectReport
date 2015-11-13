using System;

namespace ClassLibraryReport.Interfaces
{
    public interface IGroup : IComparable<IGroup>
    {
        String Name { get; set; }
        String On { get; set; }
    }
}