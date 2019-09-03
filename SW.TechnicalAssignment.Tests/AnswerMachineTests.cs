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

    public class AnswerMachineTests
    {
        private readonly IFixture fixture;
        private readonly Mock<IPersonModelLoader> modelLoader;

        public AnswerMachineTests()
        {
            this.fixture = new Fixture().Customize(new AutoMoqCustomization());
            this.modelLoader = fixture.Freeze<Mock<IPersonModelLoader>>();
        }

        [Fact]
        public void CanFindUserWithId()
        {
            // Arrange
            var testPeopleData = this.fixture.CreateMany<PersonModel>().ToList();
            this.modelLoader.Setup(l => l.GetPeople()).Returns(testPeopleData);
            var expected = testPeopleData.First();

            var sut = fixture.Create<AnswerMachine>();

            // Act
            var result = sut.GetUserFullNameById(expected.Id);

            //Assert
            result.ShouldBe($"{expected.First} {expected.Last}");
        }

        [Fact]
        public void ReturnsullWithInvalidId()
        {
            //Arrange
            var testPeopleData = fixture.Create<List<PersonModel>>();
            this.modelLoader.Setup(l => l.GetPeople()).Returns(testPeopleData);

            var sut = fixture.Create<AnswerMachine>();

            // Act
            var result = sut.GetUserFullNameById(-1);

            //Assert
            result.ShouldBeNull();
        }
    }
}
