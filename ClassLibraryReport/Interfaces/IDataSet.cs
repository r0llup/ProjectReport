using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Data;

namespace ClassLibraryReport.Interfaces
{
    public interface IDataSet : ISerializable, IComparable<IDataSet>
    {
        String Name { get; set; }
        FieldDescriptors FieldDescriptors { get; set; }
        Fieldss Fieldss { get; set; }
        Stats Stats { get; set; }
        Statss Statss { get; set; }
    }
}