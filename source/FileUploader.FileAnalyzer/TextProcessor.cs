using System;
using System.IO;
using System.Web;

namespace FileUploader.FileAnalyzer
{
    internal class TextProcessor : StreamProcessorBase
    {
        internal string Delimeter = " ";

        internal override FileStatistics ComputeStatistics(HttpPostedFileBase httpPostedFileBase)
        {
            StreamReader sr = new StreamReader(httpPostedFileBase.InputStream);

            int wordCount = 0;
            int linesCount = 0;
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                if (line != null)
                {
                    line = line.Trim();
                    var fields = line.Split(Delimeter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    wordCount += fields.Length;
                    linesCount++;
                }
            }

            sr.Close();

            FileStatistics fileStatistics = new FileStatistics
            {
                WordCount = wordCount,
                LinesCount = linesCount,
                Filename = httpPostedFileBase.FileName
            };

            return fileStatistics;
        }
    }
}