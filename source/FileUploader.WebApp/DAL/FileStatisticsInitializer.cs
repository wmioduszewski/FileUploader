using System.Collections.Generic;
using System.Data.Entity;
using FileUploader.WebApp.Models;

namespace FileUploader.WebApp.DAL
{
    public class FileStatisticsInitializer : DropCreateDatabaseIfModelChanges<StatisticsContext>
    {
        protected override void Seed(StatisticsContext context)
        {
            var files = new List<FileStatisticsEntity>
            {
                new FileStatisticsEntity {Filename = "a.txt", LinesCount = 5, WordsCount = 10},
                new FileStatisticsEntity {Filename = "b.txt", LinesCount = 6, WordsCount = 40},
                new FileStatisticsEntity {Filename = "c.txt", LinesCount = 7, WordsCount = 20},
                new FileStatisticsEntity {Filename = "d.txt", LinesCount = 58, WordsCount = 100},
                new FileStatisticsEntity {Filename = "e.txt", LinesCount = 9, WordsCount = 0}
            };

            files.ForEach(x => context.FileStatisticsEntities.Add(x));
            context.SaveChanges();
        }
    }
}