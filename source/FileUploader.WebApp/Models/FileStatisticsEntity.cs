namespace FileUploader.WebApp.Models
{
    public class FileStatisticsEntity
    {
        // ReSharper disable once InconsistentNaming
        // Explanation: By default, the Entity Framework interprets a property 
        // that's named ID or classnameID as the primary key
        public int ID { get; set; }
        public string Filename { get; set; }
        public int WordsCount { get; set; }
        public int LinesCount { get; set; }
    }
}