using Microsoft.EntityFrameworkCore;
using NastyaKupcovakt_42_21.Database;
using NastyaKupcovakt_42_21.Interfaces;
using NastyaKupcovakt_42_21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NastyaKupcovakt_42_21.Filters;
using NastyaKupcovakt_42_21.Filters.StudentFilters;

namespace nastya_kupcova_kt_42_21.Tests
{
    public class FIOTest
    {
        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;

        public FIOTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
            .UseInMemoryDatabase(databaseName: "student_db")
            .Options;
        }

        [Fact]
        public async Task GetStudentsByFIOAsync_KT3120_OneObjects()
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

            var filterFIO = new NastyaKupcovakt_42_21.Filters.StudentFilters.StudentFIOFilter
            {
                F = "С",
                I = "С",
                O = "B",
            };
            var studentsResult = await studentService.GetStudentsByFIOAsync(filterFIO, CancellationToken.None);

            Assert.Equal(1, studentsResult.Length);
        }
    }
}

