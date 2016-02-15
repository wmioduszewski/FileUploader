using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileUploader.FileAnalyzer.UnitTest
{
    [TestClass]
    public class PlainTextEdgeCases
    {
        string sampleContent = "one two three four five six seven eight    ";

        [TestMethod]
        public void FewEmptyLines()
        {
            var sb = new StringBuilder();
            int lines = 5;
            for (int i = 0; i < lines; i++)
            {
                sb.Append(Environment.NewLine);
            }

            var httpInput = Utils.PrepareFakeWebInput(sb.ToString());
            var stats = Utils.GetProductionStatistics(httpInput);

            Assert.AreEqual(stats.WordsCount, 0);
            Assert.AreEqual(stats.LinesCount, lines);
        }

        [TestMethod]
        public void OneLineWithFewWords()
        {
            var httpInput = Utils.PrepareFakeWebInput(sampleContent);
            var stats = Utils.GetProductionStatistics(httpInput);

            Assert.AreEqual(stats.WordsCount, 8);
            Assert.AreEqual(stats.LinesCount, 1);
        }

        [TestMethod]
        public void EmptyContent()
        {
            string content = string.Empty;
            var httpInput = Utils.PrepareFakeWebInput(content);
            var stats = Utils.GetProductionStatistics(httpInput);

            Assert.AreEqual(stats.WordsCount, 0);
            Assert.AreEqual(stats.LinesCount, 0);
        }

        [TestMethod]
        public void EachWordInNewLine()
        {
            var sb = new StringBuilder();
            var words = sampleContent.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                sb.Append(word);
                sb.Append(Environment.NewLine);
            }

            var httpInput = Utils.PrepareFakeWebInput(sb.ToString());
            var stats = Utils.GetProductionStatistics(httpInput);

            Assert.AreEqual(stats.WordsCount, 8);
            Assert.AreEqual(stats.LinesCount, 8);
        }
    }
}