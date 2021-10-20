using AutoFixture;
using AutoFixture.Xunit2;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using RIL.Models;
using System.Threading.Tasks;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Services
{
    public class AuthServiceUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserIdAndToken_WhenConfirmEmailAsync_ThenIdentityResultReturned(string userId, string token, [Frozen] IUnitOfWork service, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns(user);
            A.CallTo(() => service.ExtendedUsers.ConfirmEmailAsync(user, token))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.ConfirmEmailAsync(userId, token);

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserIdAndToken_WhenConfirmEmailAsync_ThenFailedIdentityResultReturned(string userId, string token, [Frozen] IUnitOfWork service, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId))
                .Returns<ExtendedUser>(null);

            //Act
            var result = await sut.ConfirmEmailAsync(userId, token);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUser_WhenGenerateConfirmationLinkAsync_ThenConfirmationLinkReturned([Frozen] IUnitOfWork service, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();
            var confirmationLink = fixture.Create<string>();

            A.CallTo(() => service.ExtendedUsers.GenerateEmailConfirmationTokenAsync(user))
                .Returns(confirmationLink);

            //Act
            var result = await sut.GenerateConfirmationLinkAsync(user);

            //Assert
            Assert.Equal(confirmationLink, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserIdAndConfirmationLink_WhenSendConfirmationLinkAsync_ThenNothingReturned(string userId, string confirmationLink, [Frozen] IEmailSenderService service, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var htmlMessage = "<p><a href=\"" + confirmationLink + "\">Follow the link to confirm your email</a></p>";

            //Act
            await sut.SendConfirmationLinkAsync(userId, confirmationLink);

            //Assert
            A.CallTo(() => service.SendEmailByMailKitAsync(userId, htmlMessage))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenEmailPasswordIssuerAudienceAndKey_WhenSignInUserAsync_ThenUserAndTokenReturned(string email, string password, string issuer, string audience, string key, [Frozen] IValidatorService validatorService, [Frozen] IUnitOfWork service, [Frozen] ITokenService tokenService, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();
            var signInResult = SignInResult.Success;
            var tokenString = fixture.Create<string>();

            A.CallTo(() => validatorService.IsValidEmail(email))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns(user);
            A.CallTo(() => validatorService.IsValidPassword(password))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.PasswordSignInAsync(user, password))
                .Returns(signInResult);
            A.CallTo(() => tokenService.BuildToken(user, issuer, audience, key))
                .Returns(tokenString);

            //Act
            var result = await sut.SignInUserAsync(email, password, issuer, audience, key);

            //Assert
            Assert.Equal((user, tokenString), result);
        }


        [Theory]
        [InlineAutoFakeItEasyData(false)]
        [InlineAutoFakeItEasyData(true, false)]
        [InlineAutoFakeItEasyData(true, true, null)]
        public async Task GivenEmailPasswordIssuerAudienceAndKey_WhenSignInUserAsync_ThenNullReturned(bool isValidEmail, bool isValidPassword, string tokenString ,string email, string password, string issuer, string audience, string key, [Frozen] IValidatorService validatorService, [Frozen] IUnitOfWork service, [Frozen] ITokenService tokenService, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();
            var signInResult = SignInResult.Success;

            A.CallTo(() => validatorService.IsValidEmail(email))
                .Returns(isValidEmail);
            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns(user);
            A.CallTo(() => validatorService.IsValidPassword(password))
                .Returns(isValidPassword);
            A.CallTo(() => service.ExtendedUsers.PasswordSignInAsync(user, password))
                .Returns(signInResult);
            A.CallTo(() => tokenService.BuildToken(user, issuer, audience, key))
                .Returns(tokenString);

            //Act
            var result = await sut.SignInUserAsync(email, password, issuer, audience, key);

            //Assert
            Assert.Equal((null, null), result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenFailedSignInResult_WhenSignInUserAsync_ThenNullReturned(string email, string password, string issuer, string audience, string key, [Frozen] IValidatorService validatorService, [Frozen] IUnitOfWork service, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();
            var signInResult = SignInResult.Failed;

            A.CallTo(() => validatorService.IsValidEmail(email))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns(user);
            A.CallTo(() => validatorService.IsValidPassword(password))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.PasswordSignInAsync(user, password))
                .Returns(signInResult);

            //Act
            var result = await sut.SignInUserAsync(email, password, issuer, audience, key);

            //Assert
            Assert.Equal((null, null), result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNonExistentUser_WhenSignInUserAsync_ThenNullReturned(string email, string password, string issuer, string audience, string key, [Frozen] IValidatorService validatorService, [Frozen] IUnitOfWork service, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            A.CallTo(() => validatorService.IsValidEmail(email))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns<ExtendedUser>(null);

            //Act
            var result = await sut.SignInUserAsync(email, password, issuer, audience, key);

            //Assert
            Assert.Equal((null, null), result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenEmailAndPassword_WhenSignUpUserAsync_ThenUserReturned(string email, string password, [Frozen] IValidatorService validatorService, [Frozen] IUnitOfWork service, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var newUser = fixture.Create<ExtendedUser>();
            var isUserCreated = IdentityResult.Success;

            A.CallTo(() => validatorService.IsValidEmail(email))
                .Returns(true);
            A.CallTo(() => validatorService.IsValidPassword(password))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.CreateForSignUp(email))
                .Returns(newUser);
            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns<ExtendedUser>(null);
            A.CallTo(() => service.ExtendedUsers.CreateAsync(newUser, password))
                .Returns(isUserCreated);
            A.CallTo(() => service.ExtendedUsers.AddToRoleAsync(newUser, "User"))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.SignUpUserAsync(email, password);

            //Assert
            Assert.Equal(newUser, result);
        }

        [Theory]
        [InlineAutoFakeItEasyData(false)]
        [InlineAutoFakeItEasyData(true, false)]
        [InlineAutoFakeItEasyData(true, true, null)]
        public async Task GivenEmailAndPassword_WhenSignUpUserAsync_ThenNullReturned(bool isValidEmail, bool isValidPassword, string email, string password, [Frozen] IValidatorService validatorService, [Frozen] IUnitOfWork service, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();
            var newUser = fixture.Create<ExtendedUser>();

            A.CallTo(() => validatorService.IsValidEmail(email))
                .Returns(isValidEmail);
            A.CallTo(() => validatorService.IsValidPassword(password))
                .Returns(isValidPassword);
            A.CallTo(() => service.ExtendedUsers.CreateForSignUp(email))
                .Returns(newUser);
            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns(user);

            //Act
            var result = await sut.SignUpUserAsync(email, password);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenFailedSignUpResult_WhenSignUpUserAsync_ThenNullReturned(string email, string password, [Frozen] IValidatorService validatorService, [Frozen] IUnitOfWork service, AuthService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var newUser = fixture.Create<ExtendedUser>();
            var isUserCreated = IdentityResult.Failed(null);

            A.CallTo(() => validatorService.IsValidEmail(email))
                .Returns(true);
            A.CallTo(() => validatorService.IsValidPassword(password))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.CreateForSignUp(email))
                .Returns(newUser);
            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns<ExtendedUser>(null);
            A.CallTo(() => service.ExtendedUsers.CreateAsync(newUser, password))
                .Returns(isUserCreated);
            A.CallTo(() => service.ExtendedUsers.AddToRoleAsync(newUser, "User"))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.SignUpUserAsync(email, password);

            //Assert
            Assert.Null(result);
        }
    }
}
