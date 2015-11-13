using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.Interfaces
{
    public interface ISeriess : ISerializable, IComparable<ISeriess>
    {
        String Name { get; set; }
    }
}