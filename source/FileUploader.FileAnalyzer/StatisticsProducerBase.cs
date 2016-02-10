using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FileUploader.FileAnalyzer
{
    internal abstract class StatisticsProducerBase
    {
        internal abstract void GetStatistics(HttpPostedFileBase file);
    }
}
