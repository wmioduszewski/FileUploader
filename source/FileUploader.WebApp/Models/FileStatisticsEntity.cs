using FileUploader.FileAnalyzer;

namespace FileUploader.WebApp.Models
{
    public class FileStatisticsEntity
    {

        public FileStatisticsEntity()
        {
            
        }

        public FileStatisticsEntity(FileStatistics fileStatistics)
        {
            LinesCount = fileStatistics.LinesCount;
            WordsCount = fileStatistics.WordsCount;
            Filename = fileStatistics.Filename;
        }

        // ReSharper disable once InconsistentNaming
        // Explanation: By default, the Entity Framework interprets a property 
        // that's named ID or classnameID as the primary key
        public int ID { get; set; }
        public string Filename { get; private set; }
        public int WordsCount { get; private set; }
        public int LinesCount { get; private set; }
    }
}