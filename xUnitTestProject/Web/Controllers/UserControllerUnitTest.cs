using BLL.Interfaces;
using BLL.ViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Web.Controllers;
using Xunit;
using System.Web;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace xUnitTestProject.Web.Controllers
{
    public class UserControllerUnitTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        [Fact]
        public void UpdateUserProfileAsync__()
        {

        }

        [Fact]
        public void UpdateUserPasswordAsync__()
        {

        }

        [Fact]
        public async Task GetUserProfileAsync_ReturnsOkObjectResult_WithReturnUserProfileViewModel()
        {
            ////Arrange
            ////var cookies = new HttpCookieCollection();

            //var fakeUserService = A.Fake<IUserService>();
            //var fakeMemoryCache = A.Fake<IMemoryCache>();
            //var controller = new UserController(fakeUserService, fakeMemoryCache);

            ////Act
            //var result = await controller.GetUserProfileAsync();

            ////Assert
            //var actionReasult = Assert.IsType<OkObjectResult>(result);
            //var model = Assert.IsAssignableFrom<ReturnUserProfileViewModel>(actionReasult.Value);
            //Assert.NotNull(model);
        }
    }
}
