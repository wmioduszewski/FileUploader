using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;

namespace FileUploader.FileAnalyzer
{
    internal class TextProcessor : StreamProcessorBase
    {
        internal override IList<string> SupportedExtensions
        {
            get { return new List<string> {".txt"}; }
        }

        internal override FileStatistics ComputeStatistics(HttpPostedFileBase httpPostedFileBase)
        {
            StreamReader sr = new StreamReader(httpPostedFileBase.InputStream);
            var delimeters = GetDelimeters();

            int wordCount = 0;
            int linesCount = 0;
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                if (line != null)
                {
                    line = line.Trim();
                    var fields = line.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                    wordCount += fields.Length;
                    linesCount++;
                }
            }

            sr.Close();

            FileStatistics fileStatistics = new FileStatistics
            {
                WordsCount = wordCount,
                LinesCount = linesCount,
                Filename = httpPostedFileBase.FileName
            };

            return fileStatistics;
        }

        private char[] GetDelimeters()
        {
            char[] delimeters;

            try
            {
                delimeters = ConfigurationManager.AppSettings["TxtDelimeters"].ToCharArray();
            }
            catch (Exception)
            {
                //default delimeter
                delimeters = " ".ToCharArray();
            }

            return delimeters;
        }
    }
}