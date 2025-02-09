
using Domain.Entities.Ad;
using FluentAssertions;

namespace Adly.Domain.Tests.AdTests
{
    public class AddEntityTest
    {
        [Fact]
        public void Creating_Ads_With_Null_UserId_Should_Throw_Exception()
        {
            //Arrange
            var description = "test desc";
            var name = "Ad Name";
            Guid? userId = null;
            Guid categoryId = Guid.NewGuid();
            Guid? locationId = Guid.NewGuid();

            //Act
            Action act = () => AdEntity.Create(name, description, userId, categoryId, locationId);

            //Assert
            act.Should().Throw<ArgumentException>();


        }

        [Fact]
        public void Creating_Ads_With_Empty_UserId_Should_Throw_Exception()
        {
            //Arrange
            var description = "test desc";
            var name = "Ad Name";
            Guid? userId = Guid.Empty;
            Guid categoryId = Guid.NewGuid();
            Guid? locationId = Guid.NewGuid();

            //Act
            Action act = () => AdEntity.Create(name, description, userId, categoryId, locationId);

            //Assert
            act.Should().Throw<ArgumentException>();


        }

        [Fact]
        public void Creating_Ads_With_Null_CategoryId_Should_Throw_Exception()
        {
            //Arrange
            var description = "test desc";
            var name = "Ad Name";
            Guid? userId = Guid.NewGuid();
            Guid? categoryId = null;
            Guid? locationId = Guid.NewGuid();

            //Act
            Action act = () => AdEntity.Create(name, description, userId, categoryId, locationId);

            //Assert
            act.Should().Throw<ArgumentException>();

        }

        [Fact]
        public void Creating_Ads_With_Empty_CategoryId_Should_Throw_Exception()
        {
            //Arrange
            var description = "test desc";
            var name = "Ad Name";
            Guid? userId = Guid.NewGuid();
            Guid? categoryId = Guid.Empty;
            Guid? locationId = Guid.NewGuid();

            //Act
            Action act = () => AdEntity.Create(name, description, userId, categoryId, locationId);

            //Assert
            act.Should().Throw<ArgumentException>();

        }

        [Fact]
        public void Creating_An_Ad_Should_Have_ChangeLog()
        {
            //Arrange
            var description = "test desc";
            var name = "Ad Name";
            Guid? userId = Guid.NewGuid();
            Guid? categoryId = Guid.NewGuid();
            Guid? locationId = Guid.NewGuid();

            //Act
            var ad = AdEntity.Create(name, description, userId, categoryId, locationId);

            //Assert
            ad.ChangeLogs.Should().HaveCount(1);
        }

        [Fact]
        public void Chaning_Ad_State_From_Approved_To_Rejected_States_Is_NotAllowed()
        {
            //Arrange
            var description = "test desc";
            var name = "Ad Name";
            Guid? userId = Guid.NewGuid();
            Guid? categoryId = Guid.NewGuid();
            Guid? locationId = Guid.NewGuid();

            //Act
            var ad = AdEntity.Create(name, description, userId, categoryId, locationId);

            //Assert

            ad.ChangeStates(AdEntity.AdStates.Approved);

            var result = ad.ChangeStates(AdEntity.AdStates.Rejected);

            result.IsSuccess.Should().BeFalse();

        }
        [Fact]
        public void Chaning_Ad_Stated_Should_Log()
        {
            //Arrange
            var description = "test desc";
            var name = "Ad Name";
            Guid? userId = Guid.NewGuid();
            Guid? categoryId = Guid.NewGuid();
            Guid? locationId = Guid.NewGuid();

            //Act
            var ad = AdEntity.Create(name, description, userId, categoryId, locationId);

            var changeLogCount = ad.ChangeLogs.Count();

            //Assert

            ad.ChangeStates(AdEntity.AdStates.Approved);

            ad.ChangeLogs.Count().Should().BeGreaterThan(changeLogCount);

        }
        [Fact]
        public void Two_Categories_With_SameId_Must_Be_Equal()
        {
            //Arrange
            var description = "test desc";
            var name = "Ad Name";
            Guid? userId = Guid.NewGuid();
            Guid? categoryId = Guid.NewGuid();
            Guid? id = Guid.NewGuid();
            Guid? locationId = Guid.NewGuid();

            //Act 
            var Ad1 = AdEntity.Create(id, name, description, userId, categoryId, locationId);
            var Ad2 = AdEntity.Create(id, name, description, userId, categoryId, locationId);

            Ad1.Equals(Ad2).Should().BeTrue();

        }

        [Fact]
        public void Ad_State_Should_Be_Pending_After_Edit()
        {
            //Arrange
            var description = "test desc";
            var name = "Ad Name";
            Guid? userId = Guid.NewGuid();
            Guid? categoryId = Guid.NewGuid();
            Guid? id = Guid.NewGuid();
            Guid? locationId = Guid.NewGuid();

            //Act 
            var Ad = AdEntity.Create(id, name, description, userId, categoryId, locationId);
            Ad.ChangeStates(AdEntity.AdStates.Approved);

            Ad.Edit("new title", "new description", categoryId, locationId);


            Ad.CurrentState.Should().Be(AdEntity.AdStates.Pending);
        }

        [Fact]
        public void Should_Create_Log_After_Editing_An_Ad()
        {
            //Arrange
            var description = "test desc";
            var name = "Ad Name";
            Guid? userId = Guid.NewGuid();
            Guid? categoryId = Guid.NewGuid();
            Guid? id = Guid.NewGuid();
            Guid? locationId = Guid.NewGuid();

            //Act 
            var Ad = AdEntity.Create(id, name, description, userId, categoryId, locationId);
            Ad.ChangeStates(AdEntity.AdStates.Approved);

            var currentChangeLogCount = Ad.ChangeLogs.Count();

            Ad.Edit("new title", "new description", categoryId, locationId);


            Ad.ChangeLogs.Count().Should().BeGreaterThan(currentChangeLogCount);
        }
    }
}
