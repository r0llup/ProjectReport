using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TablixColumns : ISerializable
    {
        public TablixColumns() : this(default(TablixColumnsGrouping))
        {
        }

        public TablixColumns(String sorting, Boolean reorderable, Boolean hideable) :
            this(null, sorting, reorderable, hideable)
        {
        }

        public TablixColumns(TablixColumnsGrouping tablixColumnsGrouping) :
            this(tablixColumnsGrouping, null)
        {
        }

        public TablixColumns(TablixColumnsGrouping tablixColumnsGrouping, String sorting) :
            this(tablixColumnsGrouping, sorting, true)
        {
        }

        public TablixColumns(TablixColumnsGrouping tablixColumnsGrouping, String sorting,
                             Boolean reorderable) :
                                 this(tablixColumnsGrouping, sorting, reorderable, true)
        {
        }

        public TablixColumns(TablixColumnsGrouping tablixColumnsGrouping, String sorting,
                             Boolean reorderable, Boolean hideable)
        {
            TablixColumnsGrouping = tablixColumnsGrouping;
            Sorting = sorting;
            Reorderable = reorderable;
            Hideable = hideable;
        }

        public TablixColumns(TablixColumns tablixColumns)
        {
            TablixColumnsGrouping = new TablixColumnsGrouping(tablixColumns.TablixColumnsGrouping);
            Sorting = tablixColumns.Sorting;
            Reorderable = tablixColumns.Reorderable;
            Hideable = tablixColumns.Hideable;
        }

        public TablixColumns(SerializationInfo si, StreamingContext sc)
        {
            TablixColumnsGrouping = si.GetValue("TablixColumnsGrouping",
                                                typeof (TablixColumnsGrouping)) as TablixColumnsGrouping;
            Sorting = si.GetValue("Sorting", typeof (String)) as String;
            Reorderable = (Boolean) si.GetValue("Reorderable", typeof (Boolean));
            Hideable = (Boolean) si.GetValue("Hideable", typeof (Boolean));
        }

        public TablixColumnsGrouping TablixColumnsGrouping { get; set; }
        public String Sorting { get; set; }
        public Boolean Reorderable { get; set; }
        public Boolean Hideable { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("TablixColumnsGrouping", TablixColumnsGrouping);
            si.AddValue("Sorting", Sorting);
            si.AddValue("Reorderable", Reorderable);
            si.AddValue("Hideable", Hideable);
        }

        public override String ToString()
        {
            return String.Format("[ Column ]{0}[ Sorting: {1} ][ Reorderable: {2} ]" +
                                 "[ Hideable: {3} ]",
                                 TablixColumnsGrouping, Sorting, Reorderable, Hideable);
        }
    }
}