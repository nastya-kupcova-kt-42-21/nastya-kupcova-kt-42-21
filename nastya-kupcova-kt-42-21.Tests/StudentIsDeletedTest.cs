using Microsoft.EntityFrameworkCore;
using NastyaKupcovakt_42_21.Database;
using NastyaKupcovakt_42_21.Filters.StudentFilters;
using NastyaKupcovakt_42_21.Interfaces;
using NastyaKupcovakt_42_21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nastya_kupcova_kt_42_21.Tests
{
    public class StudentIsDeletedTest
    {
        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;
        public StudentIsDeletedTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
            .UseInMemoryDatabase(databaseName: "studentdel_db")
            .EnableSensitiveDataLogging()
            .Options;
        }
        [Fact]
        public async Task GetStudentsByIsDeletedAsync_True_TwoObjects()
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
        Surname = "A",
        Name = "A",
        Midname = "A",
        GroupId = 3, // Соответствует "КТ-31-20"
        IsDeleted = false,
    },
    new Student
    {
        Surname = "B",
        Name = "B",
        Midname = "B",
        GroupId = 3, // Соответствует "КТ-31-20"
        IsDeleted = false,
    },
    new Student
    {
        Surname = "C",
        Name = "C",
        Midname = "C",
        GroupId = 1, // Соответствует "КТ-42-21"
        IsDeleted = true, // Будет проигнорирован
    }
    };
            await ctx.Set<Student>().AddRangeAsync(students);

            await ctx.SaveChangesAsync();

            // Act
            var filter = new StudentIsDeletedFilter
            {
                StudentIsDeleted = true,

            };

            var studentsResult = await studentService.GetStudentsByIsDeletedAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(0, studentsResult.Length);
        }
    }
}
