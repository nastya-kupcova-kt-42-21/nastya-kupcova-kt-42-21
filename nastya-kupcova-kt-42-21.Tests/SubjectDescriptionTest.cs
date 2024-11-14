using NLog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NastyaKupcovakt_42_21.Database;
using NastyaKupcovakt_42_21.Interfaces;
using NastyaKupcovakt_42_21.Models;
using NastyaKupcovakt_42_21.Filters;
using NastyaKupcovakt_42_21.Filters.SubjectsFilters;
using NastyaKupcovakt_42_21.Filters.StudentFilters;
using Microsoft.AspNetCore.Mvc;

namespace nastya_kupcova_kt_42_21.Tests
{
    public class SubjectDescriptionTest
    {
        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;
        public SubjectDescriptionTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
            .UseInMemoryDatabase(databaseName: "subject_db")
            .EnableSensitiveDataLogging()
            .Options;
        }

        [Fact]
        public async Task GetSubjectsByDescriptionAsync_eee_OneObjects()
        {
            var ctx = new StudentDbContext(_dbContextOptions);
            var subjectService = new SubjectService(ctx);
            var subjects = new List<Subject>
        {

        new Subject
        {
            SubjectName = "e",
            SubjectDescription = "eee",
            //SubjectIsDeleted = false,
        },

        new Subject
        {
            SubjectName = "e",
            SubjectDescription = "ede",
        },

        new Subject
        {
            SubjectName = "a",
            SubjectDescription = "aaa",
        }
        };

            await ctx.Set<Subject>().AddRangeAsync(subjects);

            await ctx.SaveChangesAsync();

            var filter = new SubjectDescriptionFilter
            {
                SubjectDescription = "eee",

            };

            var subjectsResult = await subjectService.GetSubjectsByDescriptionAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(1, subjectsResult.Length);

        }
    }
}
