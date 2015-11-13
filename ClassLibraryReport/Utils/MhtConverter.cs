using System;
using ADODB;
using CDO;

namespace ClassLibraryReport.Utils
{
    public static class MhtConverter
    {
        public static void Convert(String htmlFilename, String mhtFilename)
        {
            var objMessage = new Message();
            objMessage.CreateMHTMLBody(htmlFilename);
            var strm = new Stream {Type = StreamTypeEnum.adTypeText, Charset = "US-ASCII"};
            strm.Open();
            IDataSource dsk = objMessage.DataSource;
            dsk.SaveToObject(strm, "_Stream");
            strm.SaveToFile(mhtFilename, SaveOptionsEnum.adSaveCreateOverWrite);
            strm.Close();
        }
    }
}