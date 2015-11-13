using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;

namespace ClassLibraryReport.Interfaces
{
    internal interface IStats : ISerializable
    {
        Datas<Object> Firsts { get; set; }
        Datas<Object> Lasts { get; set; }
        Datas<Object> Avgs { get; set; }
        Datas<Object> Mins { get; set; }
        Datas<Object> Maxs { get; set; }
        Datas<Object> Sums { get; set; }
        Datas<Object> Counts { get; set; }
        Datas<Object> StDevs { get; set; }
        Datas<Object> Vars { get; set; }
    }
}