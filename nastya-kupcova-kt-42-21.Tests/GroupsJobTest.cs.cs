using Microsoft.EntityFrameworkCore;
using NastyaKupcovakt_42_21.Database;
using NastyaKupcovakt_42_21.Filters.GroupFilters;
//using NastyaKupcovakt_42_21.Filters.StudentFilters;
using NastyaKupcovakt_42_21.Interfaces;
using NastyaKupcovakt_42_21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nastya_kupcova_kt_42_21.Tests
{
    public class GroupsJobTest
    {
        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;
        public GroupsJobTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
            .UseInMemoryDatabase(databaseName: "groupjob_db")
            .EnableSensitiveDataLogging()
            .Options;
        }
        [Fact]
        public async Task GetGroupsByYearAsync_Job42_OneObject() // Изменяем имя теста
        {
            // Arrange
            var ctx = new StudentDbContext(_dbContextOptions);
            var groupService = new GroupService(ctx); // Используем GroupService
            var groups = new List<Group>
            {
                new Group
                {
                    GroupName = "IVT",
                    GroupJob = "31",
                    GroupYear = "20",
                },
                new Group
                {
                    GroupName = "КТ",
                    GroupJob = "31",
                    GroupYear = "21",
                }

            };
            await ctx.Set<Group>().AddRangeAsync(groups);
            await ctx.SaveChangesAsync();

            // Act
            var filter = new GroupJobFilter
            {
                GroupJob = "41", 
            };

            var groupsResult = await groupService.GetGroupsByJobAsync(filter, CancellationToken.None); // Используем метод GetGroupsByYearAsync

            // Assert
            Assert.Equal(0, groupsResult.Length); 
        }

    }
}
