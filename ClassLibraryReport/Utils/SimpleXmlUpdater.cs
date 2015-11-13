using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Xml;

namespace ClassLibraryReport.Utils
{
    public static class SimpleXmlUpdater
    {
        public static void Update(DataSet dataSet, String xmlFilePath)
        {
            Update(dataSet, xmlFilePath, null);
        }

        public static void Update(List<DataSet> dataSets, String xmlFilePath)
        {
            Update(dataSets, xmlFilePath, null);
        }

        public static void Update(List<DataSet> dataSets, String xmlFilePath, String reportName)
        {
            foreach (DataSet dataSet in dataSets)
                Update(dataSet, xmlFilePath, reportName);
        }

        public static void Update(DataSet dataSet, String xmlFilePath, String reportName)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);
            XmlNode xmlNodeDataSets = reportName == null
                                          ? xmlDocument.SelectSingleNode("/Reports/Report/DataSets")
                                          : xmlDocument.SelectSingleNode(
                                              String.Format("/Reports/Report[attribute::Name='{0}']/DataSets",
                                                            reportName));
            if (xmlNodeDataSets != null)
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    XmlElement xmlNodeDataSet = xmlDocument.CreateElement("DataSet");
                    XmlAttribute xmlAttributeDataSetName = xmlDocument.CreateAttribute("Name");
                    xmlAttributeDataSetName.Value = dataTable.TableName;
                    xmlNodeDataSet.RemoveAll();
                    xmlNodeDataSet.Attributes.Append(xmlAttributeDataSetName);
                    XmlElement xmlElementFields = xmlDocument.CreateElement("Fields");
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        XmlElement xmlElementField = xmlDocument.CreateElement("Field");
                        XmlAttribute xmlAttributeFieldId = xmlDocument.CreateAttribute("Id");
                        xmlAttributeFieldId.Value = dataColumn.ColumnName;
                        XmlAttribute xmlAttributeFieldType = xmlDocument.CreateAttribute("Type");
                        xmlAttributeFieldType.Value = dataColumn.DataType.ToString();
                        xmlElementField.Attributes.Append(xmlAttributeFieldId);
                        xmlElementField.Attributes.Append(xmlAttributeFieldType);
                        xmlElementFields.AppendChild(xmlElementField);
                    }
                    xmlNodeDataSet.AppendChild(xmlElementFields);
                    xmlNodeDataSets.AppendChild(xmlNodeDataSet);
                }
            xmlDocument.Save(xmlFilePath);
        }

        public static void Update(List<Object> objectList, String xmlFilePath)
        {
            Update(objectList, xmlFilePath, null);
        }

        public static void Update(List<Object> objectList, String xmlFilePath, String reportName)
        {
            Update(objectList, xmlFilePath, reportName, null);
        }

        public static void Update(List<Object> objectList, String xmlFilePath, String reportName,
                                  String objectListName)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);
            XmlNode xmlNodeDataSets = reportName == null
                                          ? xmlDocument.SelectSingleNode("/Reports/Report/DataSets")
                                          : xmlDocument.SelectSingleNode(
                                              String.Format("/Reports/Report[attribute::Name='{0}']/DataSets",
                                                            reportName));
            if (xmlNodeDataSets != null)
            {
                XmlNode xmlNodeDataSet = xmlNodeDataSets.FirstChild ?? xmlDocument.CreateElement("DataSet");
                XmlAttribute xmlAttributeDataSetName = xmlDocument.CreateAttribute("Name");
                xmlAttributeDataSetName.Value = objectListName ?? GetMemberName(() => objectList);
                xmlNodeDataSet.RemoveAll();
                if (xmlNodeDataSet.Attributes != null)
                    xmlNodeDataSet.Attributes.Append(xmlAttributeDataSetName);
                XmlElement xmlElementFields = xmlDocument.CreateElement("Fields");
                foreach (object obj in objectList)
                {
                    XmlElement xmlElementField = xmlDocument.CreateElement("Field");
                    XmlAttribute xmlAttributeFieldId = xmlDocument.CreateAttribute("Id");
                    object obj1 = obj;
                    xmlAttributeFieldId.Value = GetMemberName(() => obj1);
                    XmlAttribute xmlAttributeFieldType = xmlDocument.CreateAttribute("Type");
                    xmlAttributeFieldType.Value = obj.GetType().ToString();
                    xmlElementField.Attributes.Append(xmlAttributeFieldId);
                    xmlElementField.Attributes.Append(xmlAttributeFieldType);
                    xmlElementFields.AppendChild(xmlElementField);
                }
                xmlNodeDataSet.AppendChild(xmlElementFields);
                xmlNodeDataSets.AppendChild(xmlNodeDataSet);
            }
            xmlDocument.Save(xmlFilePath);
        }

        public static void Update(Object[] headersName, String xmlFilePath)
        {
            Update(headersName, xmlFilePath, null);
        }

        public static void Update(Object[] headersName, String xmlFilePath, String reportName)
        {
            Update(headersName, xmlFilePath, reportName, null);
        }

        public static void Update(Object[] headersName, String xmlFilePath, String reportName,
                                  String tableName)
        {
            Update(headersName, xmlFilePath, reportName, tableName, null);
        }

        public static void Update(Object[] headersName, String xmlFilePath, String reportName,
                                  String tableName, String dataSet)
        {
            Update(headersName, xmlFilePath, reportName, tableName, dataSet, null);
        }

        public static void Update(Object[] headersName, String xmlFilePath, String reportName,
                                  String tableName, String dataSet, Object[] footersName)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);
            XmlNode xmlNodeReportItems = reportName == null
                                             ? xmlDocument.SelectSingleNode("/Reports/Report/Body/ReportItems")
                                             : xmlDocument.SelectSingleNode(
                                                 String.Format(
                                                     "/Reports/Report[attribute::Name='{0}']/Body/ReportItems",
                                                     reportName));
            if (xmlNodeReportItems != null)
            {
                XmlNodeList xmlNodeListTablix = xmlDocument.GetElementsByTagName("Tablix");
                XmlNode xmlNodeTablix = xmlNodeListTablix.Count > 0
                                            ? xmlNodeListTablix[0]
                                            : xmlDocument.CreateElement("Tablix");
                if (xmlNodeTablix != null)
                {
                    XmlAttribute xmlAttributeTableName = xmlDocument.CreateAttribute("Name");
                    xmlAttributeTableName.Value = tableName;
                    XmlAttribute xmlAttributeTableDataSet = xmlDocument.CreateAttribute("DataSet");
                    xmlAttributeTableDataSet.Value = dataSet;
                    xmlNodeTablix.RemoveAll();
                    if (xmlNodeTablix.Attributes != null)
                    {
                        xmlNodeTablix.Attributes.Append(xmlAttributeTableName);
                        xmlNodeTablix.Attributes.Append(xmlAttributeTableDataSet);
                    }
                    if (headersName != null)
                    {
                        XmlElement xmlElementHeaders = xmlDocument.CreateElement("Headers");
                        foreach (object headerName in headersName)
                        {
                            XmlElement xmlElementHeader = xmlDocument.CreateElement("Header");
                            XmlAttribute xmlAttributeHeaderValue = xmlDocument.CreateAttribute("Value");
                            xmlAttributeHeaderValue.Value = !headerName.Equals(DBNull.Value)
                                                                ? headerName as String
                                                                : "No Value :/";
                            XmlAttribute xmlAttributeHeaderType = xmlDocument.CreateAttribute("Type");
                            xmlAttributeHeaderType.Value = "System.String";
                            xmlElementHeader.Attributes.Append(xmlAttributeHeaderValue);
                            xmlElementHeader.Attributes.Append(xmlAttributeHeaderType);
                            xmlElementHeaders.AppendChild(xmlElementHeader);
                        }
                        xmlNodeTablix.AppendChild(xmlElementHeaders);
                    }
                    if (footersName != null)
                    {
                        XmlElement xmlElementFooters = xmlDocument.CreateElement("Footers");
                        foreach (object footerName in footersName)
                        {
                            XmlElement xmlElementFooter = xmlDocument.CreateElement("Footer");
                            XmlAttribute xmlAttributeFooterValue = xmlDocument.CreateAttribute("Value");
                            xmlAttributeFooterValue.Value = footerName as String;
                            XmlAttribute xmlAttributeFooterType = xmlDocument.CreateAttribute("Type");
                            xmlAttributeFooterType.Value = footerName.GetType().ToString();
                            xmlElementFooter.Attributes.Append(xmlAttributeFooterValue);
                            xmlElementFooter.Attributes.Append(xmlAttributeFooterType);
                            xmlElementFooters.AppendChild(xmlElementFooter);
                        }
                        xmlNodeTablix.AppendChild(xmlElementFooters);
                    }
                    xmlNodeReportItems.AppendChild(xmlNodeTablix);
                }
            }
            xmlDocument.Save(xmlFilePath);
        }

        public static String GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            var expressionBody = (MemberExpression) memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
}