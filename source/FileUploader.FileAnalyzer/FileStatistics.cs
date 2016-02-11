namespace FileUploader.FileAnalyzer
{
    public class FileStatistics
    {
        public string Filename { get; set; }
        public int WordCount { get; set; }
        public int LinesCount { get; set; }
        public int ContentLength { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} words, {2} lines", Filename, WordCount, LinesCount);
        }
    }
}