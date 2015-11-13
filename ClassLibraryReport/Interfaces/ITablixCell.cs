using System;
using ClassLibraryReport.View;

namespace ClassLibraryReport.Interfaces
{
    public interface ITablixCell : IDisplayable
    {
        String Id { get; set; }
        String RowSpan { get; set; }
        String ColSpan { get; set; }
        Boolean Header { get; set; }
        TextBox TextBox { get; set; }
    }
}