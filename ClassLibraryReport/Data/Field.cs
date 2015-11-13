using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.Data
{
    [Serializable]
    public class Field : IField
    {
        public Field() : this(default(Object))
        {
        }

        public Field(Object value)
        {
            Value = value;
        }

        public Field(Field field)
        {
            Value = field.Value;
        }

        public Field(SerializationInfo si, StreamingContext sc)
        {
            Value = si.GetValue("Value", typeof (Object));
        }

        public Object Value { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Value", Value);
        }

        public override String ToString()
        {
            return String.Format("[ Field ][ Value: {0} ]", Value);
        }
    }
}