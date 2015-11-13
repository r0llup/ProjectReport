using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ClassLibraryReport.Parsers;
using ClassLibraryReport.Renderers;
using ClassLibraryReport.Utils;
using ClassLibraryReport.View;

namespace DataExample
{
    public class CMain
    {
        public static void Main()
        {
            var dsPrest = new DSPrestation();
            var prestNbUniteSum = new List<Int32> {0, 0};
            for (int index = 0; index < 10; index++)
            {
                var rn = dsPrest.Tables[0].NewRow() as DSPrestation.TableRow;
                if (rn == null) continue;
                rn.prest_pk = new Random().Next(99999);
                rn.prest_date = DateTime.Now.AddDays(index + 1);
                rn.prest_description = "blabla" + index;
                rn.prest_nb_unite = index + 1;
                rn.prest_taux = Convert.ToDecimal((index + 1)*1.13);
                rn.prest_user_fk = index%2 == 0 ? 10 : 20;
                rn.user_nom = index%2 == 0 ? "Alain" : "Bernard";
                rn.user_initiales = index%2 == 0 ? "AF" : "BD";
                if (rn.user_initiales.Equals("AF"))
                    prestNbUniteSum[0] += rn.prest_nb_unite;
                else
                    prestNbUniteSum[1] += rn.prest_nb_unite;
                dsPrest.Tables[0].Rows.Add(rn);
            }
            for (int index2 = 0; index2 < 2; index2++)
            {
                var rn = dsPrest.Tables[1].NewRow() as DSPrestation.ChartTestingRow;
                if (rn == null) continue;
                rn.user_initiales_sum = index2%2 == 0 ? "AF" : "BD";
                rn.prest_nb_unite_sum = prestNbUniteSum[index2];
                dsPrest.Tables[1].Rows.Add(rn);
            }
            //var dsPrest2 = DsUpdater.Update(dsPrest, new List<String> { "prest_type_fk" });
            SimpleXmlUpdater.Update(dsPrest, String.Format("Xml{0}Reports.xml", Path.DirectorySeparatorChar),
                                    "Prestation de test");
            Report r = DsConverter.Convert(dsPrest); //, "user_initiales");
            var sxp = new SimpleXmlParser(String.Format("Xml{0}Reports.xml", Path.DirectorySeparatorChar),
                                          String.Format("Schemas{0}Reports.xsd", Path.DirectorySeparatorChar), r);
            sxp.Parse();
            var hg = new HtmlRenderer(r, "Css", "JsLib",
                                      String.Format("Translations{0}Reports_fr_FR.pyt", Path.DirectorySeparatorChar),
                                      "Htmls", "Mhtmls");
            hg.Render();
            Process.Start(String.Format("{0}{1}Htmls{2}{3}.html", Directory.GetCurrentDirectory(),
                                        Path.DirectorySeparatorChar, Path.DirectorySeparatorChar, r.Name));
            /*MhtConverter.Convert(String.Format("{0}{1}{2}.html", Directory.GetCurrentDirectory(),
                Path.DirectorySeparatorChar, r.Name),
                String.Format("{0}{1}{2}.mht", Directory.GetCurrentDirectory(),
                Path.DirectorySeparatorChar, r.Name));*/
        }
    }
}