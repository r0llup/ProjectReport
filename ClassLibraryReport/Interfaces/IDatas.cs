using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClassLibraryReport.Interfaces
{
    public interface IDatas<T> : ISerializable
    {
        List<T> DataList { get; set; }
        void AddData(T data);
        void RemoveData(T data);
        void RemoveDataByValue(Object dataValue);
        void RemoveDataByName(String dataName);
        void ModifyData(Object dataValue, T newData);
        void ModifyData(String dataName, T newData);
        void RemoveAllData();
        Int32 ContainsDataById(String dataId);
        Boolean IsDataListEmpty();
    }
}