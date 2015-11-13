using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.Interfaces
{
    public interface ISeries : ISerializable, IComparable<ISeries>
    {
        String Name { get; set; }
        String Type { get; set; }
    }
}