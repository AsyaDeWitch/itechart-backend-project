using BLL.ViewModels;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL
{
    public class ViewModelsUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void GivenAssignRoleToUserViewModel_WhenGetProperties_ThenPropertiesReturned(AssignRoleToUserViewModel sut)
        {
            // Arrange

            //Act
            var email = sut.Email;
            var roleName = sut.RoleName;

            //Assert
            Assert.NotNull(email);
            Assert.NotNull(roleName);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenAuthViewModel_WhenGetProperties_ThenPropertiesReturned(AuthViewModel sut)
        {
            // Arrange

            //Act
            var email = sut.Email;
            var password = sut.Password;

            //Assert
            Assert.NotNull(email);
            Assert.NotNull(password);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenOrderProductsViewModel_WhenGetProperties_ThenPropertiesReturned(OrderProductsViewModel sut)
        {
            // Arrange

            //Act
            var order = sut.Order;
            var products = sut.Products;

            //Assert
            Assert.NotNull(order);
            Assert.NotNull(products);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenReturnProductOrderViewModel_WhenGetProperties_ThenPropertiesReturned(ReturnProductOrderViewModel sut)
        {
            // Arrange

            //Act
            var returnOrderViewModel = sut.ReturnOrderViewModel;
            var productOrderViewModels = sut.ProductOrderViewModels;

            //Assert
            Assert.NotNull(returnOrderViewModel);
            Assert.NotNull(productOrderViewModels);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenReturnProductViewModel_WhenGetProperties_ThenPropertiesReturned(ReturnProductViewModel sut)
        {
            // Arrange

            //Act
            var id = sut.Id;
            var name = sut.Name;
            var platform = sut.Platform;
            var dateCreated = sut.DateCreated;
            var totalRating = sut.TotalRating;
            var genre = sut.Genre;
            var rating = sut.Rating;
            var logo = sut.Logo;
            var background = sut.Background;
            var price = sut.Price;
            var count = sut.Count;



            //Assert
            Assert.NotEmpty(id.ToString());
            Assert.NotNull(name);
            Assert.NotNull(platform);
            Assert.NotEmpty(dateCreated.ToString());
            Assert.NotEmpty(totalRating.ToString());
            Assert.NotNull(genre);
            Assert.NotNull(rating);
            Assert.NotNull(logo);
            Assert.NotNull(background);
            Assert.NotEmpty(price.ToString());
            Assert.NotEmpty(count.ToString());
        }
    }
}
