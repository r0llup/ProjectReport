using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TablixBody : TablixContainer
    {
        public TablixBody()
        {
        }

        public TablixBody(TablixRows tablixRows) : base(tablixRows)
        {
        }

        public TablixBody(TablixRows tablixRows, Boolean linked) :
            base(tablixRows, linked)
        {
        }

        public TablixBody(TablixBody tablixBody) : base(tablixBody)
        {
        }

        public TablixBody(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }

        public override String ToString()
        {
            return String.Format("[ TablixBody ]{0}", base.ToString());
        }
    }
}