using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.View
{
    [Serializable]
    public abstract class TablixContainer : ISerializable
    {
        protected TablixContainer() : this(new TablixRows())
        {
        }

        protected TablixContainer(TablixRows tablixRows) :
            this(tablixRows, false)
        {
        }

        protected TablixContainer(TablixRows tablixRows, Boolean linked)
        {
            TablixRows = tablixRows;
            Linked = linked;
        }

        protected TablixContainer(TablixContainer tablixContainer)
        {
            TablixRows = new TablixRows(tablixContainer.TablixRows);
            Linked = tablixContainer.Linked;
        }

        protected TablixContainer(SerializationInfo si, StreamingContext sc)
        {
            TablixRows = si.GetValue("TablixRows", typeof (TablixRows)) as TablixRows;
            Linked = (Boolean) si.GetValue("Linked", typeof (Boolean));
        }

        public TablixRows TablixRows { get; set; }
        public Boolean Linked { get; set; }

        public void GetObjectData(SerializationInfo si, StreamingContext sc)
        {
            si.AddValue("TablixRows", TablixRows);
            si.AddValue("Linked", Linked);
        }

        public override String ToString()
        {
            return String.Format("[ Linked: {0} ]", Linked);
        }
    }
}