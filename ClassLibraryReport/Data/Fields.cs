using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;

namespace ClassLibraryReport.Data
{
    [Serializable]
    public class Fields : Datas<Field>
    {
        public Fields()
        {
        }

        public Fields(List<Field> fieldList) : base(fieldList)
        {
        }

        public Fields(Fields fields) : base(fields)
        {
        }

        public Fields(SerializationInfo si, StreamingContext sc) :
            base(si, sc)
        {
        }
    }
}