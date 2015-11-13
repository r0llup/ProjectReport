using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.Data
{
    public class Seriess : Datas<Series>, ISeriess
    {
        public Seriess() : this(new List<Series>())
        {
        }

        public Seriess(List<Series> seriesList) : this(seriesList, null)
        {
        }

        public Seriess(List<Series> seriesList, String name) : base(seriesList)
        {
            Name = name;
        }

        public Seriess(Seriess seriess) : base(seriess)
        {
            Name = seriess.Name;
        }

        public Seriess(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
            Name = si.GetValue("Name", typeof (String)) as String;
        }

        public String Name { get; set; }

        public override void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            base.GetObjectData(si, sc);
            si.AddValue("Name", Name);
        }

        public Int32 CompareTo(ISeriess seriess)
        {
            return seriess is Seriess
                       ? String.Compare(Name, seriess.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ Seriess ][ Name: {0} ]", Name);
        }
    }
}