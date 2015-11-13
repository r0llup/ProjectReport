using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.Interfaces
{
    public interface IDisplayable : ISerializable, IComparable<IDisplayable>
    {
        String Name { get; set; }
        String Style { get; set; }
        String Tag { get; set; }
    }
}