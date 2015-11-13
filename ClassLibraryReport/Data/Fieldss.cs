using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;

namespace ClassLibraryReport.Data
{
    [Serializable]
    public class Fieldss : Datas<Fields>
    {
        public Fieldss()
        {
        }

        public Fieldss(List<Fields> fieldsList) : base(fieldsList)
        {
        }

        public Fieldss(Fieldss fieldss) : base(fieldss)
        {
        }

        public Fieldss(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }
    }
}