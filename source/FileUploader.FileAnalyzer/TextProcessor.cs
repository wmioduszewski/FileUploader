using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FileUploader.FileAnalyzer
{
    internal class TextProcessor : StreamProcessorBase
    {
        internal override FileStatistics ComputeStatistics(HttpPostedFileBase httpPostedFileBase)
        {
            throw new NotImplementedException();
        }
    }
}
