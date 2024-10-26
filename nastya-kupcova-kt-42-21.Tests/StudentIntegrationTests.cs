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
            .Options;
        }
        [Fact]
        public async Task GetStudentsByGroupAsync_KT421_TwoObjects()
        {
            // Arrange
            var ctx = new StudentDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);
            var groups = new List<Group>
            {
                new Group
                {
                    GroupName = "KT-42-21"
                },
                new Group
                {
                    GroupName = "KT-44-21"
                }
            };
            await ctx.Set<Group>().AddRangeAsync(groups);
            var students = new List<Student>
            {
                new Student
                {
                    Surname = "qwerty",
                    Name = "asdf",
                    Midname = "zxc",
                    GroupId = 1,
                },
                new Student
                {
                    Surname = "qwerty2",
                    Name = "asdf2",
                    Midname = "zxc2",
                    GroupId = 2,
                },
                new Student
                {
                    Surname = "qwerty3",
                    Name = "asdf3",
                    Midname = "zxc3",
                    GroupId = 1,
                }
            };
            await ctx.Set<Student>().AddRangeAsync(students);
            await ctx.SaveChangesAsync();
            // Act
            var filter = new NastyaKupcovakt_42_21.Filters.StudentFilters.StudentGroupFilter
            {
                GroupName = "KT-42-21"
            };

            var studentsResult = await studentService.GetStudentsByGroupAsync(filter, CancellationToken.None);
            // Assert
            Assert.Equal(2, studentsResult.Length);
        }
    }
}
