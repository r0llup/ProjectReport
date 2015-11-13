using System;
using System.Data;
using ClassLibraryReport.Data;

namespace ClassLibraryReport.Utils
{
    public static class StatsHelper
    {
        public static String[] AggregateFunctions =
            {
                "FIRST", "LAST", "AVG", "MIN", "MAX", "SUM", "COUNT", "STDEV", "VAR"
            };

        public static Stats Help(DataTable dataTable, Stats stats)
        {
            GetFirsts(dataTable, ref stats);
            GetLasts(dataTable, ref stats);
            GetAvgs(dataTable, ref stats);
            GetMins(dataTable, ref stats);
            GetMaxs(dataTable, ref stats);
            GetSums(dataTable, ref stats);
            GetCounts(dataTable, ref stats);
            GetStDevs(dataTable, ref stats);
            GetVars(dataTable, ref stats);
            return stats;
        }

        public static void GetFirsts(DataTable dataTable, ref Stats stats)
        {
            if (dataTable == null || stats == null) return;
            foreach (DataColumn dataColumn in dataTable.Columns)
                stats.Firsts.AddData(dataTable.Rows[0][dataColumn.ColumnName]);
        }

        public static void GetLasts(DataTable dataTable, ref Stats stats)
        {
            if (dataTable == null || stats == null) return;
            foreach (DataColumn dataColumn in dataTable.Columns)
                stats.Lasts.AddData(dataTable.Rows[dataTable.Rows.Count - 1][dataColumn.ColumnName]);
        }

        public static void GetAvgs(DataTable dataTable, ref Stats stats)
        {
            if (dataTable == null || stats == null) return;
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                if (dataColumn.DataType != Type.GetType("System.String") &&
                    dataColumn.DataType != Type.GetType("System.DateTime"))
                    stats.Avgs.AddData(dataTable.Compute(
                        String.Format("Avg({0})", dataColumn.ColumnName), String.Empty));
                else
                    stats.Avgs.AddData("N/A");
            }
        }

        public static void GetMins(DataTable dataTable, ref Stats stats)
        {
            if (dataTable == null || stats == null) return;
            foreach (DataColumn dataColumn in dataTable.Columns)
                stats.Firsts.AddData(dataTable.Compute(
                    String.Format("Min({0})", dataColumn.ColumnName), String.Empty));
        }

        public static void GetMaxs(DataTable dataTable, ref Stats stats)
        {
            if (dataTable == null || stats == null) return;
            foreach (DataColumn dataColumn in dataTable.Columns)
                stats.Firsts.AddData(dataTable.Compute(
                    String.Format("Max({0})", dataColumn.ColumnName), String.Empty));
        }

        public static void GetSums(DataTable dataTable, ref Stats stats)
        {
            if (dataTable == null || stats == null) return;
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                if (dataColumn.DataType != Type.GetType("System.String") &&
                    dataColumn.DataType != Type.GetType("System.DateTime"))
                    stats.Sums.AddData(dataTable.Compute(
                        String.Format("Sum({0})", dataColumn.ColumnName), String.Empty));
                else
                    stats.Sums.AddData("N/A");
            }
        }

        public static void GetCounts(DataTable dataTable, ref Stats stats)
        {
            if (dataTable == null || stats == null) return;
            foreach (DataColumn dataColumn in dataTable.Columns)
                stats.Counts.AddData(dataTable.Compute(
                    String.Format("Count({0})", dataColumn.ColumnName), String.Empty));
        }

        public static void GetStDevs(DataTable dataTable, ref Stats stats)
        {
            if (dataTable == null || stats == null) return;
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                if (dataColumn.DataType != Type.GetType("System.String") &&
                    dataColumn.DataType != Type.GetType("System.DateTime"))
                    stats.StDevs.AddData(dataTable.Compute(
                        String.Format("StDev({0})", dataColumn.ColumnName), String.Empty));
                else
                    stats.StDevs.AddData("N/A");
            }
        }

        public static void GetVars(DataTable dataTable, ref Stats stats)
        {
            if (dataTable == null || stats == null) return;
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                if (dataColumn.DataType != Type.GetType("System.String") &&
                    dataColumn.DataType != Type.GetType("System.DateTime"))
                    stats.Vars.AddData(dataTable.Compute(
                        String.Format("Var({0})", dataColumn.ColumnName), String.Empty));
                else
                    stats.Vars.AddData("N/A");
            }
        }
    }
}