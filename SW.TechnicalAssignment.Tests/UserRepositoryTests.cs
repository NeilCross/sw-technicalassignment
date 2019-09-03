namespace SW.TechnicalAssignment.Tests
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Moq;
    using Shouldly;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    using DataAccess;
    using Models;
    using Interfaces;

    public class UserRepositoryTests
    {
        private readonly IFixture fixture;
        private readonly Mock<IUserDataSource> mockDataSource;

        public UserRepositoryTests()
        {
            this.fixture = new Fixture().Customize(new AutoMoqCustomization());
            this.mockDataSource = fixture.Freeze<Mock<IUserDataSource>>();
        }

        [Fact]
        public void GetByIdCanFindUserWithId()
        {
            // Arrange
            var testData = this.fixture.Create<List<UserModel>>();
            this.mockDataSource.Setup(l => l.GetUsers()).Returns(testData);

            var expected = testData.First();

            var sut = fixture.Create<UserRepository>();

            // Act
            var result = sut.GetById(expected.Id);

            //Assert
            result.ShouldBe(expected);
        }

        [Fact]
        public void GetByIdReturnsullWithInvalidId()
        {
            // Arrange
            var testData = this.fixture.Create<List<UserModel>>();
            this.mockDataSource.Setup(l => l.GetUsers()).Returns(testData);

            var sut = fixture.Create<UserRepository>();

            // Act
            var result = sut.GetById(-1);

            //Assert
            result.ShouldBeNull();
        }

        [Theory]
        [InlineData(34, 1)]
        [InlineData(32, 2)]
        [InlineData(26, 0)]
        public void GetUsersByAgeReturnsUsersWithCorrectAge(int testAge, int expectedUsers)
        {
            // Arrange
            var testData = new List<UserModel>()
            {
                new UserModel { Age = 32 },
                new UserModel { Age = 34 },
                new UserModel { Age = 35 },
                new UserModel { Age = 32 },
                new UserModel { Age = 36 },
            };
            this.mockDataSource.Setup(l => l.GetUsers()).Returns(testData);

            var sut = fixture.Create<UserRepository>();

            // Act
            var result = sut.GetUsersByAge(testAge);

            // Assert
            result.ShouldAllBe(u => u.Age == testAge);
            result.Count().ShouldBe(expectedUsers);
        }



        [Fact]
        public void GenerateAgeSummariesAccumulatesGenderCounts()
        {
            // Arrange
            var testData = new List<UserModel>()
            {
                new UserModel { Age = 33, Gender = Gender.Female },
                new UserModel { Age = 35, Gender = Gender.Male  },
                new UserModel { Age = 33, Gender = Gender.Female },
                new UserModel { Age = 34, Gender = Gender.Female},
                new UserModel { Age = 32, Gender = Gender.Male },
                new UserModel { Age = 34 },
                new UserModel { Age = 33 },
            };
            this.mockDataSource.Setup(l => l.GetUsers()).Returns(testData);

            var sut = fixture.Create<UserRepository>();

            // Act
            var result = sut.GenerateAgeSummaries();

            // Assert
            var item = result.First(s => s.Age == 33);
            item.GenderCounts[Gender.Female].ShouldBe(2);
            item.GenderCounts[Gender.Other].ShouldBe(1);
            item.GenderCounts.ContainsKey(Gender.Male).ShouldBe(false);
        }

        [Fact]
        public void GenerateAgeSummariesSortsByAge()
        {
            // Arrange
            var testData = new List<UserModel>()
            {
                new UserModel { Age = 36 },
                new UserModel { Age = 34 },
                new UserModel { Age = 31 },
                new UserModel { Age = 36 },
                new UserModel { Age = 35 },
            };
            this.mockDataSource.Setup(l => l.GetUsers()).Returns(testData);

            var sut = fixture.Create<UserRepository>();

            // Act
            var result = sut.GenerateAgeSummaries();

            // Assert
            var item = result.First();
            item.Age.ShouldBe(31);
        }
    }
}
