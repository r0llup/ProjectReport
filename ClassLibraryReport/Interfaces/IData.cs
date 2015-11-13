using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.Interfaces
{
    public interface IData : ISerializable, IComparable<IData>
    {
        String Name { get; set; }
        Object Value { get; set; }
    }
}