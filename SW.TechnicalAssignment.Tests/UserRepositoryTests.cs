namespace SW.TechnicalAssignment.Tests
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Moq;
    using Shouldly;
    using System.Collections.Generic;
    using Xunit;

    using Models;
    using Interfaces;
    using System.Linq;

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
        public void CanFindUserWithId()
        {
            // Arrange
            var testData = this.fixture.CreateMany<UserModel>().ToList();
            this.mockDataSource.Setup(l => l.GetUsers()).Returns(testData);

            var expected = testData.First();

            var sut = fixture.Create<UserRepository>();

            // Act
            var result = sut.GetById(expected.Id);

            //Assert
            result.ShouldBe(expected);
        }

        [Fact]
        public void ReturnsullWithInvalidId()
        {
            // Arrange
            var testData = fixture.Create<List<UserModel>>();
            this.mockDataSource.Setup(l => l.GetUsers()).Returns(testData);

            var sut = fixture.Create<UserRepository>();

            // Act
            var result = sut.GetById(-1);

            //Assert
            result.ShouldBeNull();
        }
    }
}
