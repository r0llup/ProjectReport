using System;
using System.Collections.Generic;
using ClassLibraryReport.Data;
using ClassLibraryReport.View;

namespace ClassLibraryReport.Utils
{
    public static class ObjConverter
    {
        public static Report Convert(List<List<Object>> objectsList)
        {
            return Convert(objectsList, new Report());
        }

        public static Report Convert(List<List<Object>> objectsList, Report report)
        {
            if (report == null) return null;
            var dataSet = new DataSet();
            foreach (var objectList in objectsList)
            {
                var fields = new Fields();
                foreach (object o in objectList)
                    fields.AddData(!o.Equals(DBNull.Value) ? new Field(o) : new Field("No Value :/"));
                dataSet.Fieldss.AddData(fields);
            }
            report.DataSets.AddData(dataSet);
            return report;
        }
    }
}