using AutoFixture;
using AutoFixture.Xunit2;
using BLL.Converters;
using BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using RIL.Models;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Converters
{
    public class ProductConverterUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void GivenProductViewModel_WhenConvertToAddressViewModel_ThenProductReturned(ProductViewModel product, [Frozen] IFormFile formFile, ProductConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            product.LogoImageFile = formFile;
            product.BackgroundImageFile = formFile;

            //Act
            var result = sut.ConvertToProduct(product);

            //Assert
            Assert.IsAssignableFrom<Product>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenProduct_WhenConvertToAddress_ThenReturnProductViewModelReturned(ProductConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var product = fixture.Create<Product>();

            //Act
            var result = sut.ConvertToReturnProductViewModel(product);

            //Assert
            Assert.IsAssignableFrom<ReturnProductViewModel>(result);
        }
    }
}

