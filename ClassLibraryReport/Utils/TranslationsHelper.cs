using System;
using System.Collections.Generic;
using System.IO;

namespace ClassLibraryReport.Utils
{
    public static class TranslationsHelper
    {
        public static Dictionary<String, String> Help(String translationsFilePath)
        {
            if (translationsFilePath == null || !File.Exists(translationsFilePath)) return null;
            var translationsDictionary = new Dictionary<String, String>();
            using (var sr = new StreamReader(translationsFilePath))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splittedLine = line.Split('=');
                    if (splittedLine.Length == 2 &&
                        !translationsDictionary.ContainsKey(splittedLine[0]))
                        translationsDictionary.Add(splittedLine[0], splittedLine[1]);
                }
            }
            return translationsDictionary;
        }
    }
}