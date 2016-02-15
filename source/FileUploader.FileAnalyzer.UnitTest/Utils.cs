using System.IO;
using System.Web;
using FakeItEasy;

namespace FileUploader.FileAnalyzer.UnitTest
{
    internal static class Utils
    {
        internal static string PlainTextContentType = "text/plain";

        internal static HttpPostedFileBase PrepareFakeWebInput(FileInfo file)
        {
            var fakeWebInput = A.Fake<HttpPostedFileBase>();
            var stream = File.Open(file.ToString(), FileMode.Open);
            
            A.CallTo(() => fakeWebInput.InputStream).Returns(stream);
            A.CallTo(() => fakeWebInput.FileName).Returns(file.ToString());
            A.CallTo(() => fakeWebInput.ContentType).Returns(PlainTextContentType);
            
            return fakeWebInput;
        }

        internal static HttpPostedFileBase PrepareFakeWebInput(string content)
        {
            var fakeWebInput = A.Fake<HttpPostedFileBase>();
            var stream = GenerateStreamFromString(content);
            A.CallTo(() => fakeWebInput.InputStream).Returns(stream);
            A.CallTo(() => fakeWebInput.FileName).Returns("idontcare.txt");
            return fakeWebInput;
        }

        internal static FileStatistics GetProductionStatistics(HttpPostedFileBase httpInput)
        {
            FileAnalyzerClient fileAnalyzerClient = new FileAnalyzerClient();
            var statistics = fileAnalyzerClient.ComputeStatistics(httpInput);
            httpInput.InputStream.Close();
            return statistics;
        }

        private static Stream GenerateStreamFromString(string content)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}