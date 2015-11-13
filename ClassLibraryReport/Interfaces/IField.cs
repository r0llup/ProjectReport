using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.Interfaces
{
    public interface IField : ISerializable
    {
        Object Value { get; set; }
    }
}