using System.IO;
using System.Web;
using FakeItEasy;

namespace FileUploader.FileAnalyzer.UnitTest
{
    internal static class Utils
    {
        internal static string PlainTextContentType = "text/plain";

        internal static HttpPostedFileBase PrepareFakeWebInput(FileInfo file, FileInfo filePath)
        {
            var fakeWebInput = A.Fake<HttpPostedFileBase>();
            var stream = File.Open(file.ToString(), FileMode.Open);
            
            A.CallTo(() => fakeWebInput.InputStream).Returns(stream);
            A.CallTo(() => fakeWebInput.FileName).Returns(filePath.ToString());
            A.CallTo(() => fakeWebInput.ContentType).Returns(PlainTextContentType);
            
            return fakeWebInput;
        }
    }
}