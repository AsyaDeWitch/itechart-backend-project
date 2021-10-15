using BLL.Converters;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Converters
{
    public class ConverterUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void GivenConverter_WhenGetProperties_ThenPropertiesReturned(Converter sut)
        {
            // Arrange

            //Act
            var address = sut.Address;
            var order = sut.Order;
            var productRating = sut.ProductRating;
            var productOrder = sut.ProductOrder;
            var product = sut.Product;
            var user = sut.User;

            //Assert
            Assert.NotNull(address);
            Assert.NotNull(order);
            Assert.NotNull(product);
            Assert.NotNull(productRating);
            Assert.NotNull(productOrder);
            Assert.NotNull(user);
        }
    }
}

