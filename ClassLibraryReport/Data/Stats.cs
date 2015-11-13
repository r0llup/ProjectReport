using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.Data
{
    [Serializable]
    public class Stats : IStats
    {
        public Stats() : this(new Datas<Object>())
        {
        }

        public Stats(Datas<Object> firsts) : this(firsts, new Datas<Object>())
        {
        }

        public Stats(Datas<Object> firsts, Datas<Object> lasts) :
            this(firsts, lasts, new Datas<Object>())
        {
        }

        public Stats(Datas<Object> firsts, Datas<Object> lasts, Datas<Object> avgs) :
            this(firsts, lasts, avgs, new Datas<Object>())
        {
        }

        public Stats(Datas<Object> firsts, Datas<Object> lasts, Datas<Object> avgs,
                     Datas<Object> mins) :
                         this(firsts, lasts, avgs, mins, new Datas<Object>())
        {
        }

        public Stats(Datas<Object> firsts, Datas<Object> lasts, Datas<Object> avgs,
                     Datas<Object> mins, Datas<Object> maxs) :
                         this(firsts, lasts, avgs, mins, maxs, new Datas<Object>())
        {
        }

        public Stats(Datas<Object> firsts, Datas<Object> lasts, Datas<Object> avgs,
                     Datas<Object> mins, Datas<Object> maxs, Datas<Object> sums) :
                         this(firsts, lasts, avgs, mins, maxs, sums, new Datas<Object>())
        {
        }

        public Stats(Datas<Object> firsts, Datas<Object> lasts, Datas<Object> avgs,
                     Datas<Object> mins, Datas<Object> maxs, Datas<Object> sums,
                     Datas<Object> counts) :
                         this(firsts, lasts, avgs, mins, maxs, sums, counts, new Datas<Object>())
        {
        }

        public Stats(Datas<Object> firsts, Datas<Object> lasts, Datas<Object> avgs,
                     Datas<Object> mins, Datas<Object> maxs, Datas<Object> sums,
                     Datas<Object> counts, Datas<Object> stDevs) :
                         this(firsts, lasts, avgs, mins, maxs, sums, counts, stDevs, new Datas<Object>())
        {
        }

        public Stats(Datas<Object> firsts, Datas<Object> lasts, Datas<Object> avgs,
                     Datas<Object> mins, Datas<Object> maxs, Datas<Object> sums,
                     Datas<Object> counts, Datas<Object> stDevs, Datas<Object> vars)
        {
            Firsts = new Datas<Object>(firsts);
            Lasts = new Datas<Object>(lasts);
            Avgs = new Datas<Object>(avgs);
            Mins = new Datas<Object>(mins);
            Maxs = new Datas<Object>(maxs);
            Sums = new Datas<Object>(sums);
            Counts = new Datas<Object>(counts);
            StDevs = new Datas<Object>(stDevs);
            Vars = new Datas<Object>(vars);
        }

        public Stats(Stats stats)
        {
            Firsts = new Datas<Object>(stats.Firsts);
            Lasts = new Datas<Object>(stats.Lasts);
            Avgs = new Datas<Object>(stats.Avgs);
            Mins = new Datas<Object>(stats.Mins);
            Maxs = new Datas<Object>(stats.Maxs);
            Sums = new Datas<Object>(stats.Sums);
            Counts = new Datas<Object>(stats.Counts);
            StDevs = new Datas<Object>(stats.StDevs);
            Vars = new Datas<Object>(stats.Vars);
        }

        public Stats(SerializationInfo si, StreamingContext sc)
        {
            Firsts = si.GetValue("Firsts", typeof (Datas<Object>)) as Datas<Object>;
            Lasts = si.GetValue("Lasts", typeof (Datas<Object>)) as Datas<Object>;
            Avgs = si.GetValue("Avgs", typeof (Datas<Object>)) as Datas<Object>;
            Mins = si.GetValue("Mins", typeof (Datas<Object>)) as Datas<Object>;
            Maxs = si.GetValue("Maxs", typeof (Datas<Object>)) as Datas<Object>;
            Sums = si.GetValue("Sums", typeof (Datas<Object>)) as Datas<Object>;
            Counts = si.GetValue("Counts", typeof (Datas<Object>)) as Datas<Object>;
            StDevs = si.GetValue("StDevs", typeof (Datas<Object>)) as Datas<Object>;
            Vars = si.GetValue("Vars", typeof (Datas<Object>)) as Datas<Object>;
        }

        public Datas<Object> Firsts { get; set; }
        public Datas<Object> Lasts { get; set; }
        public Datas<Object> Avgs { get; set; }
        public Datas<Object> Mins { get; set; }
        public Datas<Object> Maxs { get; set; }
        public Datas<Object> Sums { get; set; }
        public Datas<Object> Counts { get; set; }
        public Datas<Object> StDevs { get; set; }
        public Datas<Object> Vars { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Firsts", Firsts);
            si.AddValue("Lasts", Lasts);
            si.AddValue("Avgs", Avgs);
            si.AddValue("Mins", Mins);
            si.AddValue("Maxs", Maxs);
            si.AddValue("Sums", Sums);
            si.AddValue("Counts", Sums);
            si.AddValue("StDevs", Sums);
            si.AddValue("Vars", Sums);
        }
    }
}