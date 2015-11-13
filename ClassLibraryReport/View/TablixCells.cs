using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TablixCells : Datas<TablixCell>
    {
        public TablixCells()
        {
        }

        public TablixCells(List<TablixCell> tablixCellList) :
            base(tablixCellList)
        {
        }

        public TablixCells(TablixCells tablixCells) :
            base(tablixCells)
        {
        }

        public TablixCells(SerializationInfo si, StreamingContext sc) :
            base(si, sc)
        {
        }
    }
}