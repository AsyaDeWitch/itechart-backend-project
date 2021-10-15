using BLL.Interfaces;
using BLL.ViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Web.Controllers;
using Xunit;
using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.Web.Controllers
{
    public class UserControllerUnitTest
    {
        [Theory]
        [InlineAutoFakeItEasyData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI2IiwiZW1haWwiOiJpdGVjaGFydGxhYnRlc3RlckBnbWFpbC5jb20iLCJleHAiOjE2MzQyMzY0MjUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEvIn0.rxFdaZFmxG1fOOVYBJXN7erZZO6RBgoTxPle_1K0Vf8")]
        public async Task GivenUserProfileViewModel_WhenUpdateUserProfileAsync_ThenOkObjectResultWithReturnUserProfileViewModelReturned(string token, [Frozen] IUserService service, [Frozen] IMemoryCacher memoryCacher, [Frozen] IConverter converter)
        {
            //Arrange
            var sut = new UserController(service, memoryCacher, converter)
            {
                ControllerContext = new ControllerContext(),
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.Request.Headers["Cookie"] = "JwtToken=" + token + "; Path=/; Secure; HttpOnly; Expires=" +
                                            (DateTime.Now + TimeSpan.FromMinutes(120)).ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US"));

            var fixture = new Fixture();
            var userId = fixture.Create<int>();
            var stringUserId = userId.ToString();
            var user = fixture.Create<UserProfileViewModel>();
            var updatedUser = fixture.Create<ReturnUserProfileViewModel>();

            A.CallTo(() => service.GetUserId(token))
                .Returns(stringUserId);
            A.CallTo(() => service.UpdateUserProfileAsync(user, stringUserId))
                .Returns(updatedUser);

            //Act
            var result = await sut.UpdateUserProfileAsync(user);

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
            A.CallTo(() => memoryCacher.Remove(stringUserId))
                .MustHaveHappenedOnceExactly();

        }

        [Theory]
        [InlineAutoFakeItEasyData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI2IiwiZW1haWwiOiJpdGVjaGFydGxhYnRlc3RlckBnbWFpbC5jb20iLCJleHAiOjE2MzQyMzY0MjUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEvIn0.rxFdaZFmxG1fOOVYBJXN7erZZO6RBgoTxPle_1K0Vf8")]
        public async Task GivenUserProfileViewModelAndNullUpdatedUser_WhenUpdateUserProfileAsync_ThenBadRequestObjectResultReturned(string token, [Frozen] IUserService service, [Frozen] IMemoryCacher memoryCacher, [Frozen] IConverter converter)
        {
            //Arrange
            var sut = new UserController(service, memoryCacher, converter)
            {
                ControllerContext = new ControllerContext(),
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.Request.Headers["Cookie"] = "JwtToken=" + token + "; Path=/; Secure; HttpOnly; Expires=" +
                                            (DateTime.Now + TimeSpan.FromMinutes(120)).ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US"));

            var fixture = new Fixture();
            var userId = fixture.Create<int>();
            var stringUserId = userId.ToString();
            var user = fixture.Create<UserProfileViewModel>();
            
            A.CallTo(() => service.GetUserId(token))
                .Returns(stringUserId);
            A.CallTo(() => service.UpdateUserProfileAsync(user, stringUserId))
                .Returns<ReturnUserProfileViewModel>(null);

            //Act
            var result = await sut.UpdateUserProfileAsync(user);

            //Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineAutoFakeItEasyData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI2IiwiZW1haWwiOiJpdGVjaGFydGxhYnRlc3RlckBnbWFpbC5jb20iLCJleHAiOjE2MzQyMzY0MjUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEvIn0.rxFdaZFmxG1fOOVYBJXN7erZZO6RBgoTxPle_1K0Vf8")]
        public async Task GivenPatchUserPasswordViewModel_WhenUpdateUserPasswordAsync_ThenNoContentResultReturned(string token, [Frozen] IUserService service, [Frozen] IMemoryCacher memoryCacher, [Frozen] IConverter converter, [Frozen] JsonPatchDocument<PatchUserPasswordViewModel> jsonPatch)
        {
            //Arrange
            var sut = new UserController(service, memoryCacher, converter)
            {
                ControllerContext = new ControllerContext(),
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.Request.Headers["Cookie"] = "JwtToken=" + token + "; Path=/; Secure; HttpOnly; Expires=" +
                                            (DateTime.Now + TimeSpan.FromMinutes(120)).ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US"));

            var fixture = new Fixture();
            var userId = fixture.Create<int>();
            var stringUserId = userId.ToString();
            var user = jsonPatch;
            var updatedUser = fixture.Create<PatchUserPasswordViewModel>();

            A.CallTo(() => service.GetUserId(token))
                .Returns(stringUserId);
            A.CallTo(() => converter.User.ApplyTo(user))
                .Returns(updatedUser);
            A.CallTo(() => service.UpdateUserPasswordAsync(updatedUser, stringUserId))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.UpdateUserPasswordAsync(user);

            //Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
            A.CallTo(() => memoryCacher.Remove(stringUserId))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineAutoFakeItEasyData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI2IiwiZW1haWwiOiJpdGVjaGFydGxhYnRlc3RlckBnbWFpbC5jb20iLCJleHAiOjE2MzQyMzY0MjUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEvIn0.rxFdaZFmxG1fOOVYBJXN7erZZO6RBgoTxPle_1K0Vf8")]
        public async Task GivenPatchUserPasswordViewModel_WhenUpdateUserPasswordAsync_ThenBadRequestObjectResultReturned(string token, [Frozen] IUserService service, [Frozen] IMemoryCacher memoryCacher, [Frozen] IConverter converter, [Frozen] JsonPatchDocument<PatchUserPasswordViewModel> jsonPatch)
        {
            //Arrange
            var sut = new UserController(service, memoryCacher, converter)
            {
                ControllerContext = new ControllerContext(),
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.Request.Headers["Cookie"] = "JwtToken=" + token + "; Path=/; Secure; HttpOnly; Expires=" +
                                            (DateTime.Now + TimeSpan.FromMinutes(120)).ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US"));

            var fixture = new Fixture();
            var userId = fixture.Create<int>();
            var stringUserId = userId.ToString();
            var user = jsonPatch;
            var updatedUser = fixture.Create<PatchUserPasswordViewModel>();

            A.CallTo(() => service.GetUserId(token))
                .Returns(stringUserId);
            A.CallTo(() => converter.User.ApplyTo(user))
                .Returns(updatedUser);
            A.CallTo(() => service.UpdateUserPasswordAsync(updatedUser, stringUserId))
                .Returns(IdentityResult.Failed(null));

            //Act
            var result = await sut.UpdateUserPasswordAsync(user);

            //Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            A.CallTo(() => memoryCacher.Remove(stringUserId))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineAutoFakeItEasyData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI2IiwiZW1haWwiOiJpdGVjaGFydGxhYnRlc3RlckBnbWFpbC5jb20iLCJleHAiOjE2MzQyMzY0MjUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEvIn0.rxFdaZFmxG1fOOVYBJXN7erZZO6RBgoTxPle_1K0Vf8")]
        public async Task GivenNothing_WhenGetUserProfileAsync_ThenOkObjectResultWithReturnUserProfileViewModelReturned(string token, [Frozen] IUserService service, [Frozen] IMemoryCacher memoryCacher, [Frozen] IConverter converter)
        {
            //Arrange
            var sut = new UserController(service, memoryCacher, converter)
            {
                ControllerContext = new ControllerContext(),
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.Request.Headers["Cookie"] = "JwtToken=" + token + "; Path=/; Secure; HttpOnly; Expires=" +
                                            (DateTime.Now + TimeSpan.FromMinutes(120)).ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US"));

            var fixture = new Fixture();
            var userId = fixture.Create<int>();
            var stringUserId = userId.ToString();
            var user = fixture.Create<ReturnUserProfileViewModel>();

            A.CallTo(() => service.GetUserId(token))
                .Returns(stringUserId);
            A.CallTo(() => memoryCacher.TryGetValue(stringUserId, out user))
                .Returns(false);
            A.CallTo(() => service.GetUserProfileAsync(stringUserId))
                .Returns(user);

            //Act
            var result = await sut.GetUserProfileAsync();

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
            A.CallTo(() => memoryCacher.Set(stringUserId, user))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineAutoFakeItEasyData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI2IiwiZW1haWwiOiJpdGVjaGFydGxhYnRlc3RlckBnbWFpbC5jb20iLCJleHAiOjE2MzQyMzY0MjUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEvIn0.rxFdaZFmxG1fOOVYBJXN7erZZO6RBgoTxPle_1K0Vf8")]
        public async Task GivenCacheUser_WhenGetUserProfileAsync_ThenOkObjectResultWithReturnUserProfileViewModelReturned(string token, [Frozen] IUserService service, [Frozen] IMemoryCacher memoryCacher, [Frozen] IConverter converter)
        {
            //Arrange
            var sut = new UserController(service, memoryCacher, converter)
            {
                ControllerContext = new ControllerContext(),
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext();
            sut.Request.Headers["Cookie"] = "JwtToken=" + token + "; Path=/; Secure; HttpOnly; Expires=" +
                                            (DateTime.Now + TimeSpan.FromMinutes(120)).ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US"));

            var fixture = new Fixture();
            var userId = fixture.Create<int>();
            var stringUserId = userId.ToString();
            var user = fixture.Create<ReturnUserProfileViewModel>();

            A.CallTo(() => service.GetUserId(token))
                .Returns(stringUserId);
            A.CallTo(() => memoryCacher.TryGetValue(stringUserId, out user))
                .Returns(true);

            //Act
            var result = await sut.GetUserProfileAsync();

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }
    }
}
