using AutoFixture;
using BLL.Chachers;
using BLL.ViewModels;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Cachers
{
    public class MemoryCacherUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void GivenUserId_WhenRemove_ThenNothingReturned(int userId, MemoryCacher sut)
        {
            // Arrange

            //Act
            sut.Remove(userId.ToString());

            //Assert
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenUserId_WhenTryGetValue_ThenBoolResultReturned(int userId, MemoryCacher sut)
        {
            // Arrange

            //Act
            var result = sut.TryGetValue(userId.ToString(), out _);

            //Assert
            Assert.IsAssignableFrom<bool>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenUserIdAndReturnUserProfileViewModel_WhenSet_ThenReturnUserProfileViewModelReturned(int userId, MemoryCacher sut)
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Create<ReturnUserProfileViewModel>();

            //Act
            var result = sut.Set(userId.ToString(), user);

            //Assert
            Assert.IsAssignableFrom<ReturnUserProfileViewModel>(result);
        }
    }
}
