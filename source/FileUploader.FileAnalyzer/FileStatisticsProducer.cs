using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FileUploader.FileAnalyzer
{
    internal class FileStatisticsProducer : StatisticsProducerBase
    {

        internal override void GetStatistics(HttpPostedFileBase file)
        {

            switch (file.ContentType)
            {
            }
        }
    }
}
