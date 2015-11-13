using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.Common
{
    [Serializable]
    public class Datas<T> : IDatas<T>
    {
        public Datas() : this(new List<T>())
        {
        }

        public Datas(List<T> dataList)
        {
            DataList = dataList;
        }

        public Datas(Datas<T> datas)
        {
            DataList = new List<T>(datas.DataList);
        }

        public Datas(SerializationInfo si, StreamingContext sc)
        {
            DataList = si.GetValue("DataList", typeof (List<T>)) as List<T>;
        }

        public List<T> DataList { get; set; }

        public void AddData(T data)
        {
            DataList.Add(data);
        }

        public void RemoveData(T data)
        {
            DataList.Remove(data);
        }

        public void RemoveDataByValue(Object dataValue)
        {
            for (int index = 0; index < DataList.Count; index++)
            {
                var data = DataList[index] as IData;
                if (data == null || !data.Value.Equals(dataValue)) continue;
                DataList.Remove(DataList[index]);
                break;
            }
        }

        public void RemoveDataByName(String dataName)
        {
            for (int index = 0; index < DataList.Count; index++)
            {
                var data = DataList[index] as IData;
                if (data == null || !data.Name.Equals(dataName)) continue;
                DataList.Remove(DataList[index]);
                break;
            }
        }

        public void ModifyData(Object dataValue, T newData)
        {
            RemoveDataByValue(dataValue);
            AddData(newData);
        }

        public void ModifyData(String dataName, T newData)
        {
            RemoveDataByName(dataName);
            AddData(newData);
        }

        public void RemoveAllData()
        {
            DataList.Clear();
        }

        public Int32 ContainsDataById(String dataId)
        {
            for (int index = 0; index < DataList.Count; index++)
            {
                var data = DataList[index] as IFieldDescriptor;
                if (data == null || !data.Id.Equals(dataId)) continue;
                return index;
            }
            return -1;
        }

        public Boolean IsDataListEmpty()
        {
            return DataList == null || DataList.Count <= 0;
        }

        public virtual void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("DataList", DataList);
        }
    }
}