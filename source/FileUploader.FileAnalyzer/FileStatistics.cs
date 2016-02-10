using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.FileAnalyzer
{
    public class FileStatistics
    {
        public string Filename { get; set; }
        public int WordCount { get; set; }
        public int LinesCount { get; set; }
        public int ContentLength { get; set; }
    }
}
