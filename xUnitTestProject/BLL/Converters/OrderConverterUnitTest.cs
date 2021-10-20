using AutoFixture;
using BLL.Converters;
using BLL.ViewModels;
using RIL.Models;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Converters
{
    public class OrderConverterUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void GivenOrderViewModel_WhenConvertToAddressViewModel_ThenOrderReturned(OrderConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var order = fixture.Create<OrderViewModel>();

            //Act
            var result = sut.ConvertToOrder(order);

            //Assert
            Assert.IsAssignableFrom<Order>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenOrder_WhenConvertToAddress_ThenReturnOrderViewModelReturned(OrderConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var order = fixture.Create<Order>();

            //Act
            var result = sut.ConvertToReturnOrderViewModel(order);

            //Assert
            Assert.IsAssignableFrom<ReturnOrderViewModel>(result);
        }
    }
}

