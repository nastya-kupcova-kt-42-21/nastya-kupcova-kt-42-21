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
using NastyaKupcovakt_42_21.Filters.StudentFilters;

namespace nastya_kupcova_kt_42_21.Tests
{
    public class StudentIntegrationTests
    {
        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;
        public StudentIntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
            .UseInMemoryDatabase(databaseName: "student_db")
            .EnableSensitiveDataLogging()
            .Options;
        }
        [Fact]
        public async Task GetStudentsByGroupAsync_KT3120_TwoObjects()
        {
            // Arrange
            var ctx = new StudentDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);
            var groups = new List<Group>
    {
        new Group
        {
            GroupName = "КТ",
            GroupJob = "42",
            GroupYear = "21",
        },
        new Group
        {
            GroupName = "КТ",
            GroupJob = "41",
            GroupYear = "21",
        },
        new Group
        {
            GroupName = "КТ",
            GroupJob = "31",
            GroupYear = "20",
        }
    };
            await ctx.Set<Group>().AddRangeAsync(groups);
            var students = new List<Student>
    {
       new Student
    {
        Surname = "С",
        Name = "С",
        Midname = "С",
        GroupId = 3, // Соответствует "КТ-31-20"
        IsDeleted = false,
    },
    new Student
    {
        Surname = "С",
        Name = "С",
        Midname = "B",
        GroupId = 1, // Соответствует "КТ-31-20"
        IsDeleted = false,
    },
    new Student
    {
        Surname = "C",
        Name = "C",
        Midname = "C",
        GroupId = 3, // Соответствует "КТ-42-21"
        IsDeleted = false, // Будет проигнорирован
    }
    };
            await ctx.Set<Student>().AddRangeAsync(students);

            await ctx.SaveChangesAsync();

            // Act
            var filter = new StudentGroupFilter
            {
                GroupName = "КТ",
                GroupJob = "31",
                GroupYear = "20",

            };

            var studentsResult = await studentService.GetStudentsByGroupAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, studentsResult.Length);
        }
    }
}