using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;

namespace ClassLibraryReport.Data
{
    [Serializable]
    public class DataSets : Datas<DataSet>
    {
        public DataSets()
        {
        }

        public DataSets(List<DataSet> dataSetList) : base(dataSetList)
        {
        }

        public DataSets(DataSets dataSets) : base(dataSets)
        {
        }

        public DataSets(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }
    }
}