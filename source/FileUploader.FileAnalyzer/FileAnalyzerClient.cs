using System;
using System.Collections.Generic;
using System.Web;

namespace FileUploader.FileAnalyzer
{
    public class FileAnalyzerClient
    {
        private readonly FileStatisticsProducer _statisticsProducer;
        public IList<String> SupportedExtensions;

        public FileAnalyzerClient()
        {
            _statisticsProducer = new FileStatisticsProducer();
            SupportedExtensions = _statisticsProducer.SupportedExtensions;
        }

        public FileStatistics ComputeStatistics(HttpPostedFileBase httpPostedFileBase)
        {
            return _statisticsProducer.GetStatistics(httpPostedFileBase);
        }
    }
}