using System.IO;
using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Services
{
    public class FirebaseServiceUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenBackgroundImageFile_WhenUploadBackgroundImageAsync_ThenLinkReturned(FirebaseService sut)
        {
            // Arrange
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            await writer.WriteAsync("content");
            await writer.FlushAsync();
            stream.Position = 0;
            IFormFile image = new FormFile(stream, 0, stream.Length, "image", "image.pdf");

            //Act
            var result = await sut.UploadBackgroundImageAsync(image);

            //Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNullBackgroundImageFile_WhenUploadBackgroundImageAsync_ThenNullReturned(FirebaseService sut)
        {
            // Arrange
            var stream = new MemoryStream();
            IFormFile image = new FormFile(stream, 0, 0, "image", "image.pdf");

            //Act
            var result = await sut.UploadBackgroundImageAsync(image);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenLogoImageFile_WhenUploadLogoImageAsync_ThenLinkReturned(FirebaseService sut)
        {
            // Arrange
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            await writer.WriteAsync("content");
            await writer.FlushAsync();
            stream.Position = 0;
            IFormFile image = new FormFile(stream, 0, stream.Length, "image", "image.pdf");

            //Act
            var result = await sut.UploadLogoImageAsync(image);

            //Assert
            Assert.NotNull(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNullLogoImageFile_WhenUploadLogoImageAsync_ThenNullReturned(FirebaseService sut)
        {
            // Arrange
            var stream = new MemoryStream();
            IFormFile image = new FormFile(stream, 0, 0, "image", "image.pdf");

            //Act
            var result = await sut.UploadLogoImageAsync(image);

            //Assert
            Assert.Null(result);
        }
    }
}
