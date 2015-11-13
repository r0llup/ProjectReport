using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.Data
{
    [Serializable]
    public class Statss : Datas<Stats>, IStatss
    {
        public Statss() : this(new List<Stats>())
        {
        }

        public Statss(List<Stats> statsList) :
            this(statsList, new List<Object>())
        {
        }

        public Statss(List<Stats> statsList, List<Object> groupList) :
            base(statsList)
        {
            GroupList = groupList;
        }

        public Statss(Statss statss) : base(statss)
        {
            GroupList = new List<Object>(statss.GroupList);
        }

        public Statss(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
            GroupList = si.GetValue("GroupList", typeof (List<Object>)) as List<Object>;
        }

        public List<Object> GroupList { get; set; }

        public override void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            base.GetObjectData(si, sc);
            si.AddValue("GroupList", GroupList);
        }
    }
}