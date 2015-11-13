using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.Interfaces
{
    public interface IFieldDescriptor : ISerializable, IComparable<IFieldDescriptor>
    {
        String Id { get; set; }
        String Type { get; set; }
    }
}