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
    public class GroupYearTest
    {
        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;
        public GroupYearTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
            .UseInMemoryDatabase(databaseName: "group_db")
            .EnableSensitiveDataLogging()
            .Options;
        }
        [Fact]
        public async Task GetGroupsByYearAsync_Year20_OneObject() // Изменяем имя теста
        {
            // Arrange
            var ctx = new StudentDbContext(_dbContextOptions);
            var groupService = new GroupService(ctx); // Используем GroupService
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
                    GroupYear = "20", // Группа с годом 20
                }
            };
            await ctx.Set<Group>().AddRangeAsync(groups);
            await ctx.SaveChangesAsync();

            // Act
            var filter = new GroupYearFilter // Используем фильтр для года группы
            {
                GroupYear = "21" // Фильтруем по году 20
            };

            var groupsResult = await groupService.GetGroupsByYearAsync(filter, CancellationToken.None); // Используем метод GetGroupsByYearAsync

            // Assert
            Assert.Equal(2, groupsResult.Length); // Ожидаем 1 группу с годом 20
        }
    }
}
