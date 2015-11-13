using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TablixRows : Datas<TablixRow>
    {
        public TablixRows()
        {
        }

        public TablixRows(List<TablixRow> tablixRowList) : base(tablixRowList)
        {
        }

        public TablixRows(TablixRows tablixRows) : base(tablixRows)
        {
        }

        public TablixRows(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }
    }
}