using AutoFixture;
using BLL.Converters;
using BLL.ViewModels;
using RIL.Models;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Converters
{
    public class ProductRatingConverterUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void GivenProductRatingViewModel_WhenConvertToProductRating_ThenAddressReturned(ProductRatingConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var productRating = fixture.Create<ProductRatingViewModel>();

            //Act
            var result = sut.ConvertToProductRating(productRating);

            //Assert
            Assert.IsAssignableFrom<ProductRating>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenAddress_WhenConvertToAddress_ThenAddressViewModelReturned(ProductRatingConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var productRating = fixture.Create<ProductRating>();

            //Act
            var result = sut.ConvertToProductRatingViewModel(productRating);

            //Assert
            Assert.IsAssignableFrom<ProductRatingViewModel>(result);
        }
    }
}

