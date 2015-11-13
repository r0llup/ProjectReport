using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClassLibraryReport.Data;
using ClassLibraryReport.View;
using DataSet = System.Data.DataSet;

namespace ClassLibraryReport.Utils
{
    public static class DsConverter
    {
        public static Report Convert(DataRowCollection dataRowCollection)
        {
            return Convert(dataRowCollection, default(String));
        }

        public static Report Convert(DataRowCollection dataRowCollection, String columnNameGroup)
        {
            return Convert(dataRowCollection, new Report(), columnNameGroup);
        }

        public static Report Convert(DataSet dataSet, String tableName, String columnNameGroup)
        {
            return Convert(dataSet.Tables[tableName].Rows, columnNameGroup);
        }

        public static Report Convert(DataSet dataSet)
        {
            return Convert(dataSet, default(String));
        }

        public static Report Convert(DataSet dataSet, String columnNameGroup)
        {
            return Convert(dataSet, new Report(), columnNameGroup);
        }

        public static Report Convert(DataSet dataSet, Report report)
        {
            return Convert(dataSet, report, null);
        }

        public static Report Convert(DataSet dataSet, Report report, String columnNameGroup)
        {
            if (dataSet == null || report == null) return null;
            int counter = 0;
            if (columnNameGroup == null)
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    Convert(dataTable.Rows, report);
                    report.DataSets.DataList[counter].Stats =
                        StatsHelper.Help(dataTable, report.DataSets.DataList[counter].Stats);
                    counter++;
                }
            else
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    Convert(dataTable.Rows, report, columnNameGroup);
                    report.DataSets.DataList[counter].Stats =
                        StatsHelper.Help(dataTable, report.DataSets.DataList[counter].Stats);
                    counter++;
                }
            return report;
        }

        public static Report Convert(List<DataSet> dataSets)
        {
            return Convert(dataSets, null);
        }

        public static Report Convert(List<DataSet> dataSets, String columnNameGroup)
        {
            if (dataSets == null) return null;
            var report = new Report();
            if (columnNameGroup == null)
                foreach (DataSet dataSet in dataSets)
                    Convert(dataSet, report);
            else
                foreach (DataSet dataSet in dataSets)
                    Convert(dataSet, report, columnNameGroup);
            return report;
        }

        public static Report Convert(DataRowCollection dataRowCollection, Report report)
        {
            if (dataRowCollection == null || report == null) return null;
            var dataSet = new Data.DataSet();
            foreach (DataRow dataRow in dataRowCollection)
            {
                var fields = new Fields();
                foreach (object o in dataRow.ItemArray)
                    fields.AddData(!o.Equals(DBNull.Value) ? new Field(o) : new Field("N/A"));
                dataSet.Fieldss.AddData(fields);
            }
            report.DataSets.AddData(dataSet);
            return report;
        }

        public static Report Convert(DataRowCollection dataRowCollection, Report report, String columnNameGroup)
        {
            if (dataRowCollection == null || report == null || columnNameGroup == null) return null;
            var dataSet = new Data.DataSet();
            var columnValues = new List<Object>();
            foreach (object columnValue in dataRowCollection.Cast<DataRow>()
                                                            .Select(dataRow => dataRow[columnNameGroup])
                                                            .Where(columnValue => !columnValues.Contains(columnValue)))
                columnValues.Add(columnValue);
            foreach (object columnValue in columnValues)
            {
                var sums = new List<Double>();
                for (int index = 0; index < dataRowCollection[0].ItemArray.Count(); index++)
                    sums.Add(0d);
                foreach (DataRow dataRow in dataRowCollection)
                {
                    if (dataRow[columnNameGroup] != columnValue) continue;
                    var fields = new Fields();
                    int index = 0;
                    foreach (object o in dataRow.ItemArray)
                    {
                        fields.AddData(!o.Equals(DBNull.Value) ? new Field(o) : new Field("N/A"));
                        if (o is Decimal)
                            sums[index] += Decimal.ToDouble((Decimal) o);
                        else if (o is Int32)
                            sums[index] += (Int32) o;
                        index++;
                    }
                    dataSet.Fieldss.AddData(fields);
                }
                var sumFields = new Fields();
                foreach (double sum in sums)
                    sumFields.AddData(new Field(sum));
                dataSet.Fieldss.AddData(sumFields);
            }
            report.DataSets.AddData(dataSet);
            return report;
        }
    }
}