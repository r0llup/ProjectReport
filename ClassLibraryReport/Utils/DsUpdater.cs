using System;
using System.Collections.Generic;
using System.Data;

namespace ClassLibraryReport.Utils
{
    public static class DsUpdater
    {
        public static DataSet Update(DataSet dataSet, List<String> columnsToRemove)
        {
            var newDataSet = new DataSet();
            for (int index = 0; index < dataSet.Tables.Count; index++)
            {
                DataTable dataTable = dataSet.Tables[index];
                dataSet.Tables.Remove(dataSet.Tables[index]);
                newDataSet.Tables.Add(Update(dataTable, columnsToRemove));
            }
            return newDataSet;
        }

        public static DataSet Update(DataSet dataSet, List<Int32> columnsToRemove)
        {
            var newDataSet = new DataSet();
            for (int index = 0; index < dataSet.Tables.Count; index++)
            {
                DataTable dataTable = dataSet.Tables[index];
                dataSet.Tables.Remove(dataSet.Tables[index]);
                newDataSet.Tables.Add(Update(dataTable, columnsToRemove));
            }
            return newDataSet;
        }

        public static DataTable Update(DataTable dataTable, List<String> columnsToRemove)
        {
            if (dataTable == null || columnsToRemove == null) return dataTable;
            foreach (string columnToRemove in columnsToRemove)
                dataTable.Columns.Remove(columnToRemove);
            return dataTable;
        }

        public static DataTable Update(DataTable dataTable, List<Int32> columnsToRemove)
        {
            if (dataTable == null || columnsToRemove == null) return dataTable;
            foreach (int columnToRemove in columnsToRemove)
                dataTable.Columns.RemoveAt(columnToRemove);
            return dataTable;
        }
    }
}