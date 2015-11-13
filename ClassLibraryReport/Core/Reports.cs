using ClassLibraryReport.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClassLibraryReport.Core {
    [Serializable]
    public class Reports : Datas<Report> {
        public Reports() { }

        public Reports(List<Report> reportList) : base(reportList) { }

        public Reports(Reports reports) : base(reports) { }

        public Reports(SerializationInfo si, StreamingContext sc) : base(si, sc) { }
    }
}