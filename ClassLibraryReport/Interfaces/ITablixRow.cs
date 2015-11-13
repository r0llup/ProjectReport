using ClassLibraryReport.View;

namespace ClassLibraryReport.Interfaces
{
    public interface ITablixRow : IDisplayable
    {
        TablixCells TablixCells { get; set; }
    }
}