using System;
using ClassLibraryReport.Data;
using ClassLibraryReport.View;

namespace ClassLibraryReport.Interfaces
{
    public interface ITablix : IDisplayable
    {
        DataSet DataSet { get; set; }
        String Caption { get; set; }
        TablixBody TablixBody { get; set; }
        TablixHeader TablixHeader { get; set; }
        TablixFooter TablixFooter { get; set; }
        Boolean Dynamic { get; set; }
        Boolean Paginate { get; set; }
        TablixColumns TablixColumns { get; set; }
        TablixChart TablixChart { get; set; }
        String PaginationType { get; set; }
        TablixColumnHierarchy TablixColumnHierarchy { get; set; }
    }
}