using AutoFixture;
using AutoFixture.Xunit2;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using DAL.Interfaces;
using FakeItEasy;
using RIL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Services
{
    public class OrderServiceUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenOrderId_WhenBuyOrderAsync_ThenNothingReturned(int orderId, [Frozen] IUnitOfWork service, OrderService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var products = fixture.Create<List<ProductOrder>>();

            A.CallTo(() => service.Orders.BuyAsync(orderId))
                .Returns(true);
            A.CallTo(() => service.ProductOrders.GetProductListByOrderIdAsync(orderId))
                .Returns(products);

            //Act
            await sut.BuyOrderAsync(orderId);

            //Assert
            A.CallTo(() => service.Products.UpdateCountAsync(products))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserIdAndProducts_WhenCreateOrderAsync_ThenProductOrderReturned(int userId, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, OrderService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();
            var order = fixture.Create<Order>();
            var products = fixture.Create<ProductOrderViewModel[]>();
            var productOrders = fixture.Create<List<ProductOrder>>();
            var productList = fixture.Create<List<ProductOrderViewModel>>();
            var totalAmount = products
                .Sum(p => p.ProductAmount);

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(userId.ToString()))
                .Returns(user);
            A.CallTo(() => service.Orders.CreateAsync(user, totalAmount))
                .Returns(order);
            A.CallTo(() => converter.ProductOrder.ConvertToProductOrderList(products, order))
                .Returns(productOrders);
            A.CallTo(() => service.ProductOrders.AddProductsToOrderAsync(order.Id, productOrders))
                .Returns(productOrders);
            A.CallTo(() => converter.ProductOrder.ConvertToProductOrderViewModelList(productOrders))
                .Returns(productList);

            //Act
            var result = await sut.CreateOrderAsync(userId, products);

            //Assert
            Assert.IsAssignableFrom<ReturnProductOrderViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenOrderIdAndProducts_WhenDeleteProductsFromOrderAsync_ThenNothingReturned(int orderId, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, OrderService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var products = fixture.Create<ProductOrderViewModel[]>();
            var productOrders = fixture.Create<List<ProductOrder>>();

            A.CallTo(() => converter.ProductOrder.ConvertToProductOrderList(products))
                .Returns(productOrders);
            A.CallTo(() => service.ProductOrders.GetProductListByOrderIdAsync(orderId))
                .Returns(productOrders);
            var totalAmount = productOrders
                .Sum(p => p.ProductAmount);

            //Act
            await sut.DeleteProductsFromOrderAsync(orderId, products);

            //Assert
            A.CallTo(() => service.ProductOrders.DeleteProductsFromOrderAsync(orderId, productOrders))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => service.Orders.UpdateProductTotalAmountAsync(orderId, totalAmount))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserId_WhenGetOrderAsync_ThenProductOrderReturned(int orderId, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, OrderService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var order = fixture.Create<Order>();
            var productOrders = fixture.Create<List<ProductOrder>>();
            var productList = fixture.Create<List<ProductOrderViewModel>>();

            A.CallTo(() => service.Orders.GetByIdAsync(orderId)).Returns(order);
            A.CallTo(() => service.ProductOrders.GetProductListByOrderIdAsync(orderId))
                .Returns(productOrders);
            A.CallTo(() => converter.ProductOrder.ConvertToProductOrderViewModelList(productOrders))
                .Returns(productList);

            //Act
            var result = await sut.GetOrderAsync(orderId);

            //Assert
            Assert.IsAssignableFrom<ReturnProductOrderViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserId_WhenGetOrdersListAsync_ThenProductOrderListReturned(int userId, [Frozen] IUnitOfWork service, OrderService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var orders = fixture.Create<List<Order>>();

            A.CallTo(() => service.Orders.GetListByUserIdAsync(userId))
                .Returns(orders);

            //Act
            var result = await sut.GetOrdersListAsync(userId);

            //Assert
            Assert.IsAssignableFrom<List<ReturnOrderViewModel>>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenOrderIdOrderAndProducts_WhenUpdateOrderAsync_ThenProductOrderReturned(int orderId, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, OrderService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var order = fixture.Create<OrderViewModel>();
            var updatedOrder = fixture.Create<Order>();
            var products = fixture.Create<ProductOrderViewModel[]>();
            var productOrders = fixture.Create<List<ProductOrder>>();

            A.CallTo(() => converter.Order.ConvertToOrder(order))
                .Returns(updatedOrder);
            A.CallTo(() => service.Orders.UpdateAsync(updatedOrder))
                .Returns(updatedOrder);
            A.CallTo(() => converter.ProductOrder.ConvertToProductOrderList(products, updatedOrder))
                .Returns(productOrders);
            A.CallTo(() => service.ProductOrders.UpdateProductListInOrderAsync(orderId, productOrders))
                .Returns(productOrders);
            var totalAmount = productOrders
                .Sum(p => p.ProductAmount);

            //Act
            var result = await sut.UpdateOrderAsync(orderId, order, products);

            //Assert
            Assert.IsAssignableFrom<ReturnProductOrderViewModel>(result);
            A.CallTo(() => service.Orders.UpdateProductTotalAmountAsync(orderId, totalAmount))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNullUpdatedOrder_WhenUpdateOrderAsync_ThenProductOrderReturned(int orderId, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, OrderService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var order = fixture.Create<OrderViewModel>();

            var updatedOrder = fixture.Create<Order>();
            var products = fixture.Create<ProductOrderViewModel[]>();
            var productOrders = fixture.Create<List<ProductOrder>>();

            order.Id = orderId;
            A.CallTo(() => converter.Order.ConvertToOrder(order))
                .Returns(updatedOrder);
            A.CallTo(() => service.Orders.UpdateAsync(updatedOrder))
                .Returns<Order>(null);
            A.CallTo(() => converter.ProductOrder.ConvertToProductOrderList(products, updatedOrder))
                .Returns(productOrders);
            A.CallTo(() => service.ProductOrders.GetProductListByOrderIdAsync(orderId))
                .Returns(productOrders);

            //Act
            var result = await sut.UpdateOrderAsync(orderId, order, products);

            //Assert
            Assert.IsAssignableFrom<ReturnProductOrderViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNullOrderAddress_WhenUpdateOrderAsync_ThenProductOrderReturned(int orderId, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, OrderService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var order = fixture.Create<OrderViewModel>();
            order.AddressDelivery = null;
            var address = fixture.Create<Address>();
            //var newAddress = fixture.Create<AddressViewModel>();
            var updatedOrder = fixture.Create<Order>();
            var products = fixture.Create<ProductOrderViewModel[]>();
            var productOrders = fixture.Create<List<ProductOrder>>();
            var user = fixture.Create<ExtendedUser>();

            order.Id = orderId;
            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(order.UserId.ToString()))
                .Returns(user);
            A.CallTo(() => service.Addresses.GetByIdAsync(user.AddressDeliveryId))
                .Returns(address);
            A.CallTo(() => converter.Order.ConvertToOrder(order))
                .Returns(updatedOrder);
            A.CallTo(() => service.Orders.UpdateAsync(updatedOrder))
                .Returns<Order>(null);
            A.CallTo(() => service.ProductOrders.GetProductListByOrderIdAsync(orderId))
                .Returns(productOrders);

            //Act
            var result = await sut.UpdateOrderAsync(orderId, order, products);

            //Assert
            Assert.IsAssignableFrom<ReturnProductOrderViewModel>(result);
            A.CallTo(() => converter.Address.ConvertToAddressViewModel(address))
                .MustHaveHappenedOnceExactly();
        }
    }
}
