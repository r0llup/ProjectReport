using System;
using System.Runtime.Serialization;

namespace ClassLibraryReport.View
{
    [Serializable]
    public class TablixFooter : TablixContainer
    {
        public TablixFooter()
        {
        }

        public TablixFooter(TablixRows tablixRows) : base(tablixRows)
        {
        }

        public TablixFooter(TablixRows tablixRows, Boolean linked) :
            base(tablixRows, linked)
        {
        }

        public TablixFooter(TablixFooter tablixFooter) : base(tablixFooter)
        {
        }

        public TablixFooter(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }

        public override String ToString()
        {
            return String.Format("[ TablixFooter ]{0}", base.ToString());
        }
    }
}