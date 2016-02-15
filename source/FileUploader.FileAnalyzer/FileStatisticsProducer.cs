using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileUploader.FileAnalyzer
{
    internal class FileStatisticsProducer : StatisticsProducerBase
    {
        internal IList<String> SupportedExtensions = new List<string>();
        private readonly IList<StreamProcessorBase> _subscribedProcessors;

        public FileStatisticsProducer()
        {
            _subscribedProcessors = new List<StreamProcessorBase>();

            _subscribedProcessors.Add(new TextProcessor());
            //here you can add another StreamProcessor

            foreach (var subscribedProcessor in _subscribedProcessors)
            {
                foreach (var extension in subscribedProcessor.SupportedExtensions)
                {
                    SupportedExtensions.Add(extension);
                }
            }
        }

        internal override FileStatistics GetStatistics(HttpPostedFileBase file)
        {
            var extension = GetExtension(file.FileName);

            //I assume processors does not have any extension in common
            var streamProcessor = _subscribedProcessors.FirstOrDefault(x => x.SupportedExtensions.Contains(extension));

            if (streamProcessor == null)
                throw new Exception("Unhandled file type posted");

            return streamProcessor.ComputeStatistics(file);
        }

        private string GetExtension(string filename)
        {
            var ext = Path.GetExtension(filename);
            return ext != null ? ext.ToLower() : String.Empty;
        }
    }
}