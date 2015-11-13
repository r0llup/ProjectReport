using System.Runtime.Serialization;
using ClassLibraryReport.View;

namespace ClassLibraryReport.Interfaces
{
    public interface ITablixColumnHierarchy : ISerializable
    {
        Group Group { get; set; }
        TablixHeader TablixHeader { get; set; }
        TablixFooter TablixFooter { get; set; }
    }
}