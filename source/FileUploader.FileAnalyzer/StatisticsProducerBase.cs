using System.Web;

namespace FileUploader.FileAnalyzer
{
    internal abstract class StatisticsProducerBase
    {
        internal abstract FileStatistics GetStatistics(HttpPostedFileBase file);
    }
}