using System.Web;

namespace FileUploader.FileAnalyzer
{
    internal abstract class StreamProcessorBase
    {
        internal abstract FileStatistics ComputeStatistics(HttpPostedFileBase httpPostedFileBase);
    }
}