using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.Data
{
    public class Series : Datas<Serie>, ISeries
    {
        public Series() : this(new List<Serie>())
        {
        }

        public Series(List<Serie> serieList) : this(serieList, null)
        {
        }

        public Series(List<Serie> serieList, String name) :
            this(serieList, name, null)
        {
        }

        public Series(List<Serie> serieList, String name, String type) :
            base(serieList)
        {
            Name = name;
            Type = type;
        }

        public Series(Series series) : base(series)
        {
            Name = series.Name;
            Type = series.Type;
        }

        public Series(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
            Name = si.GetValue("Name", typeof (String)) as String;
            Type = si.GetValue("Type", typeof (String)) as String;
        }

        public String Name { get; set; }
        public String Type { get; set; }

        public override void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            base.GetObjectData(si, sc);
            si.AddValue("Name", Name);
            si.AddValue("Type", Type);
        }

        public Int32 CompareTo(ISeries series)
        {
            return series is Series
                       ? String.Compare(Name, series.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ Series ][ Name: {0} ][ Type: {1} ]", Name, Type);
        }
    }
}