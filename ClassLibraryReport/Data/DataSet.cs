using System;
using System.Runtime.Serialization;
using ClassLibraryReport.Interfaces;

namespace ClassLibraryReport.Data
{
    [Serializable]
    public class DataSet : IDataSet
    {
        public DataSet() : this(default(String))
        {
        }

        public DataSet(String name) : this(name, new FieldDescriptors())
        {
        }

        public DataSet(String name, FieldDescriptors fieldDescriptors) :
            this(name, fieldDescriptors, new Fieldss())
        {
        }

        public DataSet(String name, FieldDescriptors fieldDescriptors, Fieldss fieldss) :
            this(name, fieldDescriptors, fieldss, new Stats())
        {
        }

        public DataSet(String name, FieldDescriptors fieldDescriptors, Fieldss fieldss,
                       Stats stats) :
                           this(name, fieldDescriptors, fieldss, stats, new Statss())
        {
        }

        public DataSet(String name, FieldDescriptors fieldDescriptors, Fieldss fieldss,
                       Stats stats, Statss statss)
        {
            Name = name;
            FieldDescriptors = fieldDescriptors;
            Fieldss = fieldss;
            Stats = stats;
            Statss = statss;
        }

        public DataSet(DataSet dataSet)
        {
            Name = dataSet.Name;
            FieldDescriptors = new FieldDescriptors(dataSet.FieldDescriptors);
            Fieldss = new Fieldss(dataSet.Fieldss);
            Stats = new Stats(dataSet.Stats);
            Statss = new Statss(dataSet.Statss);
        }

        public DataSet(SerializationInfo si, StreamingContext sc)
        {
            Name = si.GetValue("Name", typeof (String)) as String;
            FieldDescriptors = si.GetValue("FieldDescriptors",
                                           typeof (FieldDescriptors)) as FieldDescriptors;
            Fieldss = si.GetValue("Fieldss", typeof (Fieldss)) as Fieldss;
            Stats = si.GetValue("Stats", typeof (Stats)) as Stats;
            Statss = si.GetValue("Statss", typeof (Statss)) as Statss;
        }

        public String Name { get; set; }
        public FieldDescriptors FieldDescriptors { get; set; }
        public Fieldss Fieldss { get; set; }
        public Stats Stats { get; set; }
        public Statss Statss { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("Name", Name);
            si.AddValue("FieldDescriptors", FieldDescriptors);
            si.AddValue("Fieldss", Fieldss);
            si.AddValue("Stats", Stats);
            si.AddValue("Statss", Statss);
        }

        public Int32 CompareTo(IDataSet dataSet)
        {
            return dataSet is DataSet
                       ? String.Compare(Name, dataSet.Name, StringComparison.Ordinal)
                       : -1;
        }

        public override String ToString()
        {
            return String.Format("[ DataSet ][ Name: {0} ]", Name);
        }
    }
}