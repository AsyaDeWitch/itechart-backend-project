using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Xunit2;
using BLL.Converters;
using BLL.ViewModels;
using RIL.Models;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Converters
{
    public class ProductOrderConverterUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void GivenProductsAndOrder_WhenConvertToProductOrderList_ThenProductOrderListReturned(ProductOrderConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var order = fixture.Create<Order>();
            var products = fixture.Create<ProductOrderViewModel[]>();

            //Act
            var result = sut.ConvertToProductOrderList(products, order);

            //Assert
            Assert.IsAssignableFrom<List<ProductOrder>>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenProducts_WhenConvertToProductOrderList_ThenProductOrderListReturned(ProductOrderConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var products = fixture.Create<ProductOrderViewModel[]>();

            //Act
            var result = sut.ConvertToProductOrderList(products);

            //Assert
            Assert.IsAssignableFrom<List<ProductOrder>>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenProductOrder_WhenConvertToProductOrderViewModel_ThenProductOrderViewModelReturned(ProductOrderConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var product = fixture.Create<ProductOrder>();

            //Act
            var result = sut.ConvertToProductOrderViewModel(product);

            //Assert
            Assert.IsAssignableFrom<ProductOrderViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenProductOrderList_WhenConvertToProductOrderViewModelList_ThenAddressViewModelReturned(ProductOrderConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var products = fixture.Create<List<ProductOrder>>();

            //Act
            var result = sut.ConvertToProductOrderViewModelList(products);

            //Assert
            Assert.IsAssignableFrom<List<ProductOrderViewModel>>(result);
        }
    }
}

