using System;
using System.Windows.Forms.DataVisualization.Charting;
using ClassLibraryReport.Data;

namespace ClassLibraryReport.Interfaces
{
    public interface IChart : IDisplayable
    {
        Seriess Seriess { get; set; }
        String ImagesDirectoryPath { get; set; }
        Chart WinFormsChart { get; set; }
        DataSet DataSet { get; set; }
        String XLabel { get; set; }
        String YLabel { get; set; }
    }
}