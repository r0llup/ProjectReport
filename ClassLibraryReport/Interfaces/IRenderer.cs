using System;
using ClassLibraryReport.View;

namespace ClassLibraryReport.Interfaces
{
    public interface IRenderer
    {
        Reports Reports { get; set; }
        void Render();
        Boolean IsValidUri(String uri);
    }
}