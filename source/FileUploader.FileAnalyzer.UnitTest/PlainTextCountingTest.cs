using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileUploader.FileAnalyzer.UnitTest
{
    [TestClass]
    public class PlainTextCountingTest
    {
        internal static string TestFilesDirectoryPath = @"TestFiles\Txt\";

        [TestMethod]
        public void StreamWordCountEqualsActualWordCount()
        {
            var fileList = ListTestFiles().ToList();

            if (!fileList.Any())
            {
                Assert.Fail("No files was configured to analysis");
            }

            foreach (var testFile in fileList)
            {
                var httpInput = Utils.PrepareFakeWebInput(new FileInfo(testFile));
                var productionFileStatistics = Utils.GetProductionStatistics(httpInput);
                var testFileStatistics = GetTestFileStatistics(testFile);
                Assert.AreEqual(productionFileStatistics.WordsCount, testFileStatistics.WordsCount);
                Assert.AreEqual(productionFileStatistics.LinesCount, testFileStatistics.LinesCount);
            }
        }

        private static IEnumerable<string> ListTestFiles()
        {
            return Directory.EnumerateFiles(TestFilesDirectoryPath);
        }

        /// <summary>
        ///     This method intentionally does the same thing as TextProcessor, but in different way, to make this test capable of
        ///     correctness validation.
        /// </summary>
        /// <param name="testFilePath">The test file path.</param>
        /// <returns></returns>
        private static FileStatistics GetTestFileStatistics(string testFilePath)
        {
            var input = File.ReadAllText(testFilePath);
            int words = Regex.Matches(input, @"[\S]+").Count;
            int lines = Regex.Matches(input, Environment.NewLine).Count;
            FileStatistics fileStatistics = new FileStatistics
            {
                LinesCount = lines,
                WordsCount = words,
                Filename = testFilePath
            };
            return fileStatistics;
        }

        private FileStatistics GetProductionFileStatistics(string testFilePath)
        {
            var filePath = new FileInfo(testFilePath);
            var fakeHttpInput = Utils.PrepareFakeWebInput(filePath);
            FileAnalyzerClient fileAnalyzerClient = new FileAnalyzerClient();
            var statistics = fileAnalyzerClient.ComputeStatistics(fakeHttpInput);
            fakeHttpInput.InputStream.Close();
            return statistics;
        }
    }
}