using AutoFixture;
using AutoFixture.Xunit2;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using DAL.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using RIL.Models;
using System.Threading.Tasks;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Services
{
    public class UserServiceUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void GivenToken_WhenGetUserId_ThenReturnUserProfileViewModelReturned(string token, [Frozen] ITokenService tokenService, UserService sut)
        {
            //Arrange

            //Act
            sut.GetUserId(token);

            //Assert
            A.CallTo(() => tokenService.ExtractUserIdFromToken(token))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserProfileAndUserId_WhenUpdateUserProfileAsync_ThenReturnUserProfileViewModelReturned(string userId, [Frozen] IUnitOfWork service, [Frozen] IValidatorService validatorService, [Frozen] IConverter converter, UserService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var userProfile = fixture.Create<UserProfileViewModel>();
            var user = fixture.Create<ExtendedUser>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns(user);
            A.CallTo(() => validatorService.IsValidPhoneNumber(userProfile.PhoneNumber))
                .Returns(true);
            user.PhoneNumber = userProfile.PhoneNumber;
            user.AddressDelivery = converter.Address.ConvertToAddress(userProfile.AddressDelivery);
            A.CallTo(() => service.ExtendedUsers.UpdateAsync(user))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.UpdateUserProfileAsync(userProfile, userId);

            //Assert
            Assert.IsAssignableFrom<ReturnUserProfileViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNullPhoneNumberAndUserName_WhenUpdateUserProfileAsync_ThenReturnUserProfileViewModelReturned(string userId, [Frozen] IUnitOfWork service, UserService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var userProfile = fixture.Create<UserProfileViewModel>();
            var user = fixture.Create<ExtendedUser>();
            var address = fixture.Create<Address>();

            userProfile.PhoneNumber = null;
            userProfile.UserName = null;
            userProfile.AddressDelivery = null;

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns(user);
            A.CallTo(() => service.Addresses.GetByIdAsync(user.AddressDeliveryId))
                .Returns(address);
            user.AddressDelivery = address;
            A.CallTo(() => service.ExtendedUsers.UpdateAsync(user))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.UpdateUserProfileAsync(userProfile, userId);

            //Assert
            Assert.IsAssignableFrom<ReturnUserProfileViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserProfileAndUserId_WhenUpdateUserProfileAsync_ThenNullReturned(string userId, [Frozen] IUnitOfWork service, [Frozen] IValidatorService validatorService, [Frozen] IConverter converter, UserService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var userProfile = fixture.Create<UserProfileViewModel>();
            var user = fixture.Create<ExtendedUser>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns(user);
            A.CallTo(() => validatorService.IsValidPhoneNumber(userProfile.PhoneNumber))
                .Returns(true);
            user.PhoneNumber = userProfile.PhoneNumber;
            user.UserName = userProfile.UserName;
            user.AddressDelivery = converter.Address.ConvertToAddress(userProfile.AddressDelivery);
            A.CallTo(() => service.ExtendedUsers.UpdateAsync(user))
                .Returns(IdentityResult.Failed(null));

            //Act
            var result = await sut.UpdateUserProfileAsync(userProfile, userId);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNonExistentUserId_WhenUpdateUserProfileAsync_ThenNullReturned(string userId, [Frozen] IUnitOfWork service, UserService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var userProfile = fixture.Create<UserProfileViewModel>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns<ExtendedUser>(null);

            //Act
            var result = await sut.UpdateUserProfileAsync(userProfile, userId);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenWrongPhoneNumber_WhenUpdateUserProfileAsync_ThenNullReturned(string userId, [Frozen] IUnitOfWork service, [Frozen] IValidatorService validatorService, UserService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var userProfile = fixture.Create<UserProfileViewModel>();
            var user = fixture.Create<ExtendedUser>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns(user);
            A.CallTo(() => validatorService.IsValidPhoneNumber(userProfile.PhoneNumber))
                .Returns(false);

            //Act
            var result = await sut.UpdateUserProfileAsync(userProfile, userId);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserId_WhenGetUserProfileAsync_ThenReturnUserProfileViewModelReturned(string userId, [Frozen] IUnitOfWork service, UserService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();
            var address = fixture.Create<Address>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns(user);
            A.CallTo(() => service.Addresses.GetByIdAsync(user.AddressDeliveryId))
                .Returns(address);

            //Act
            var result = await sut.GetUserProfileAsync(userId);

            //Assert
            Assert.IsAssignableFrom<ReturnUserProfileViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserProfileAndUserId_WhenUpdateUserPasswordAsync_ThenIdentityResultReturned(string userId, [Frozen] IUnitOfWork service, [Frozen] IValidatorService validatorService, UserService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();
            var address = fixture.Create<Address>();
            var updatedUser = fixture.Create<PatchUserPasswordViewModel>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns(user);
            A.CallTo(() => validatorService.IsValidPassword(updatedUser.NewPassword))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.ChangePasswordAsync(user, updatedUser.CurrentPassword, updatedUser.NewPassword))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.UpdateUserPasswordAsync(updatedUser, userId);

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNonExistentUserId_WhenUpdateUserPasswordAsync_ThenFailedIdentityResultReturned(string userId, [Frozen] IUnitOfWork service, UserService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var updatedUser = fixture.Create<PatchUserPasswordViewModel>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns<ExtendedUser>(null);

            //Act
            var result = await sut.UpdateUserPasswordAsync(updatedUser, userId);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenWrongPassword_WhenUpdateUserPasswordAsync_ThenFailedIdentityResultReturned(string userId, [Frozen] IUnitOfWork service, [Frozen] IValidatorService validatorService, UserService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();
            var updatedUser = fixture.Create<PatchUserPasswordViewModel>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns(user);
            A.CallTo(() => validatorService.IsValidPassword(updatedUser.NewPassword))
                .Returns(false);

            //Act
            var result = await sut.UpdateUserPasswordAsync(updatedUser, userId);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }
    }
}
