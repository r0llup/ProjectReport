using System;
using System.Xml;

namespace ClassLibraryReport.Interfaces
{
    public interface IXmlParser : IParser
    {
        String XmlFilePath { get; set; }
        String XsdFilePath { get; set; }
        XmlReaderSettings XmlReaderSettings { get; set; }
        XmlReader XmlReader { get; set; }
    }
}