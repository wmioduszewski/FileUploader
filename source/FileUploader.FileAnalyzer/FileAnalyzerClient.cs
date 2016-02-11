using System.Web;

namespace FileUploader.FileAnalyzer
{
    public class FileAnalyzerClient
    {
        public FileStatistics ComputeStatistics(HttpPostedFileBase httpPostedFileBase)
        {
            FileStatisticsProducer statisticsProducer = new FileStatisticsProducer();
            return statisticsProducer.GetStatistics(httpPostedFileBase);
        }
    }
}