using ClassLibraryReport.View;

namespace ClassLibraryReport.Interfaces
{
    public interface IParser
    {
        Reports Reports { get; set; }
        void Parse();
    }
}