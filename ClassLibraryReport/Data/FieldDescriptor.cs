using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.Data
{
    [Serializable]
    public class FieldDescriptor : IFieldDescriptor
    {
        public FieldDescriptor() : this(default(String))
        {
        }

        public FieldDescriptor(String id) : this(id, null)
        {
        }

        public FieldDescriptor(String id, String type)
        {
            Id = id;
            Type = type;
        }

        public FieldDescriptor(FieldDescriptor fieldDescriptor)
        {
            Id = fieldDescriptor.Id;
            Type = fieldDescriptor.Type;
        }

        public FieldDescriptor(SerializationInfo si, StreamingContext sc)
        {
            Id = si.GetValue("Id", typeof (String)) as String;
            Type = si.GetValue("Type", typeof (String)) as String;
        }

        public String Id { get; set; }
        public String Type { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Id", Id);
            si.AddValue("Type", Type);
        }

        public Int32 CompareTo(IFieldDescriptor fieldDescriptor)
        {
            return fieldDescriptor is FieldDescriptor
                       ? String.Compare(Id, fieldDescriptor.Id, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ FieldDescriptor ][ Id: {0} ][ Type: {1} ]",
                                 Id, Type);
        }
    }
}