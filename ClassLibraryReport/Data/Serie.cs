using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;

namespace ClassLibraryReport.Data
{
    public class Serie : Datas<Object>
    {
        public Serie()
        {
        }

        public Serie(List<Object> objectList) : base(objectList)
        {
        }

        public Serie(Serie serie) : base(serie)
        {
        }

        public Serie(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }
    }
}