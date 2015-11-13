using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Common;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class Reports : Datas<Report>
    {
        public Reports()
        {
        }

        public Reports(List<Report> reportList) : base(reportList)
        {
        }

        public Reports(Reports reports) : base(reports)
        {
        }

        public Reports(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }
    }
}