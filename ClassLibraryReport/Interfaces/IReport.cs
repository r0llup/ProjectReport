using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Data;
using ClassLibraryReport.View;

namespace ClassLibraryReport.Interfaces
{
    public interface IReport : ISerializable, IComparable<IReport>
    {
        String Name { get; set; }
        DataSets DataSets { get; set; }
        Body Body { get; set; }
        PageHeader PageHeader { get; set; }
        PageFooter PageFooter { get; set; }
        Boolean DisplayTitle { get; set; }
    }
}