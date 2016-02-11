using System;
using System.Web;

namespace FileUploader.FileAnalyzer
{
    internal class FileStatisticsProducer : StatisticsProducerBase
    {
        internal override FileStatistics GetStatistics(HttpPostedFileBase file)
        {
            StreamProcessorBase streamProcessor = null;
            switch (file.ContentType)
            {
                case "text/plain":
                    streamProcessor = new TextProcessor();
                    break;
            }

            if (streamProcessor == null)
            {
                throw new Exception("Unhandled file type posted");
            }

            return streamProcessor.ComputeStatistics(file);
        }
    }
}