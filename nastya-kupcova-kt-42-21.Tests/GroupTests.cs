using NastyaKupcovakt_42_21.Models;

namespace nastya_kupcova_kt_42_21.Tests
{
    public class GroupTests
    {
        [Fact]
        public void IsValidGroupName_KT4221_True()
        {
            //arrange
            var testGroup = new Group
            {
                GroupName = "KT-42-21"
            };
            //act
            var result = testGroup.IsValidGroupName();
            //assert
            Assert.True(result);
        }
    }
}