using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TablixHeader : TablixContainer
    {
        public TablixHeader()
        {
        }

        public TablixHeader(TablixRows tablixRows) : base(tablixRows)
        {
        }

        public TablixHeader(TablixRows tablixRows, Boolean linked) :
            base(tablixRows, linked)
        {
        }

        public TablixHeader(TablixHeader tablixHeader) : base(tablixHeader)
        {
        }

        public TablixHeader(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }

        public override String ToString()
        {
            return String.Format("[ TablixHeader ]{0}", base.ToString());
        }
    }
}