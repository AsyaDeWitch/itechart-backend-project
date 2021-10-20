using AutoFixture;
using AutoFixture.Xunit2;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using DAL.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Services
{
    public class GamesServiceUnitTest
    {
        [Theory]
        [InlineAutoFakeItEasyData(3)]
        [InlineAutoFakeItEasyData(1000)]
        public async Task GivenQuantity_WhenGetTopPlatformsAsync_ThenDictionaryResultReturned(int value, [Frozen]IUnitOfWork service, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture { RepeatCount = 900 };
            var platformAndCountPairs = fixture.Create<List<(int, int)>>();

            A.CallTo(() => service.Products.GetEachPlatformCountAsync())
                .Returns(platformAndCountPairs);

            if(value > platformAndCountPairs.Count || value <= 0)
            {
                value = platformAndCountPairs.Count;
            }
            platformAndCountPairs
                .Sort((x, y) => y.Item2.CompareTo(x.Item2));
            platformAndCountPairs = platformAndCountPairs.Take(value)
                .ToList();

            //Act
            var result = await sut.GetTopPlatformsAsync(value);

            //Assert
            Assert.All(result, item => platformAndCountPairs.Any(p => p.Item2 == item.Value));
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenParameters_WhenSearchGamesByParametersAsync_ThenListResultReturned(DateTime term, int limit, double offset, string name, [Frozen] IUnitOfWork service, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture { RepeatCount = 5};
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var gamesList = fixture.Create<List<Product>>();

            A.CallTo(() => service.Products.GetProductsByParametersAsync(term, limit, offset, name))
                .Returns(gamesList);

            //Act
            var result = await sut.SearchGamesByParametersAsync(term, limit, offset, name);

            //Assert
            Assert.Equal(result.Count, gamesList.Count);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenProductId_WhenGetProductFullInfoAsync_ThenProductReturned(int id, [Frozen] IUnitOfWork service, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var game = fixture.Create<Product>();

            A.CallTo(() => service.Products.GetByIdAsync(id))
                .Returns(game);

            //Act
            var result = await sut.GetProductFullInfoAsync(id.ToString());

            //Assert
            Assert.IsAssignableFrom<ReturnProductViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenProduct_WhenCreateProductAsync_ThenProductReturned(ProductViewModel product, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var createdProduct = fixture.Create<Product>();

            A.CallTo(() => converter.Product.ConvertToProduct(product))
                .Returns(createdProduct);
            A.CallTo(() => service.Products.CreateAsync(createdProduct))
                .Returns(createdProduct);

            //Act
            var result = await sut.CreateProductAsync(product);

            //Assert
            Assert.IsAssignableFrom<ReturnProductViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenProductWithLogoAndImageFile_WhenCreateProductAsync_ThenProductReturned(ProductViewModel product, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, [Frozen] IFirebaseService firebase, [Frozen] IFormFile formFile, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            product.LogoImageFile = formFile;
            product.BackgroundImageFile = formFile;
            var imageLink = fixture.Create<string>();
            var createdProduct = fixture.Create<Product>();

            A.CallTo(() => firebase.UploadBackgroundImageAsync(product.BackgroundImageFile))
                .Returns(imageLink);
            A.CallTo(() => firebase.UploadLogoImageAsync(product.LogoImageFile))
                .Returns(imageLink);
            A.CallTo(() => converter.Product.ConvertToProduct(product))
                .Returns(createdProduct);
            A.CallTo(() => service.Products.CreateAsync(createdProduct))
                .Returns(createdProduct);

            //Act
            var result = await sut.CreateProductAsync(product);

            //Assert
            Assert.IsAssignableFrom<ReturnProductViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenProduct_WhenUpdateProductAsync_ThenProductReturned(ProductViewModel product, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var updatedProduct = fixture.Create<Product>();

            A.CallTo(() => converter.Product.ConvertToProduct(product))
                .Returns(updatedProduct);
            A.CallTo(() => service.Products.UpdateAsync(updatedProduct))
                .Returns(updatedProduct);

            //Act
            var result = await sut.UpdateProductAsync(product);

            //Assert
            Assert.IsAssignableFrom<ReturnProductViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenProductWithLogoAndImageFile_WhenUpdateProductAsync_ThenProductReturned(ProductViewModel product, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, [Frozen] IFirebaseService firebase, [Frozen] IFormFile formFile, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            product.LogoImageFile = formFile;
            product.BackgroundImageFile = formFile;
            var imageLink = fixture.Create<string>();
            var updatedProduct = fixture.Create<Product>();

            A.CallTo(() => firebase.UploadBackgroundImageAsync(product.BackgroundImageFile))
                .Returns(imageLink);
            A.CallTo(() => firebase.UploadLogoImageAsync(product.LogoImageFile))
                .Returns(imageLink);
            A.CallTo(() => converter.Product.ConvertToProduct(product))
                .Returns(updatedProduct);
            A.CallTo(() => service.Products.UpdateAsync(updatedProduct))
                .Returns(updatedProduct);

            //Act
            var result = await sut.UpdateProductAsync(product);

            //Assert
            Assert.IsAssignableFrom<ReturnProductViewModel>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenProductWith0Id_WhenUpdateProductAsync_ThenNullReturned(ProductViewModel product, GamesService sut)
        {
            // Arrange
            product.Id = 0;

            //Act
            var result = await sut.UpdateProductAsync(product);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenProductId_WhenDeleteProductByIdAsync_ThenNothingReturned(int id, [Frozen] IUnitOfWork service, GamesService sut)
        {
            // Arrange

            //Act
            await sut.DeleteProductByIdAsync(id.ToString());

            //Assert
            A.CallTo(() => service.Products.DeleteByIdAsync(id))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenProductRating_WhenCreateProductRatingAsync_ThenProductRatingReturned(ProductRatingViewModel productRating, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var newProductRating = fixture.Create<ProductRating>();

            A.CallTo(() => converter.ProductRating.ConvertToProductRating(productRating))
                .Returns(newProductRating);
            A.CallTo(() => service.ProductRatings.CreateAsync(newProductRating))
                .Returns(newProductRating);

            //Act
            var result = await sut.CreateProductRatingAsync(productRating);

            //Assert
            Assert.IsAssignableFrom<ProductRatingViewModel>(result);
            A.CallTo(() => service.Products.UpdateTotalRatingAsync(productRating.ProductId))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenProductRating_WhenUpdateProductRatingAsync_ThenProductRatingReturned(ProductRatingViewModel productRating, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var newProductRating = fixture.Create<ProductRating>();

            A.CallTo(() => converter.ProductRating.ConvertToProductRating(productRating))
                .Returns(newProductRating);
            A.CallTo(() => service.ProductRatings.UpdateAsync(newProductRating))
                .Returns(newProductRating);

            //Act
            var result = await sut.UpdateProductRatingAsync(productRating);

            //Assert
            Assert.IsAssignableFrom<ProductRatingViewModel>(result);
            A.CallTo(() => service.Products.UpdateTotalRatingAsync(productRating.ProductId))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenRatingWith0Value_WhenUpdateProductRatingAsync_ThenNullReturned(ProductRatingViewModel productRating, GamesService sut)
        {
            // Arrange
            productRating.Rating = 0;

            //Act
            var result = await sut.UpdateProductRatingAsync(productRating);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenProductRating_WhenDeleteProductRatingAsync_ThenNothingReturned(ProductRatingViewModel productRating, [Frozen] IUnitOfWork service, [Frozen] IConverter converter, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture { RepeatCount = 10 };
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var newProductRating = fixture.Create<ProductRating>();

            A.CallTo(() => converter.ProductRating.ConvertToProductRating(productRating))
                .Returns(newProductRating);

            //Act
            await sut.DeleteProductRatingAsync(productRating);

            //Assert
            A.CallTo(() => service.ProductRatings.DeleteAsync(newProductRating))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => service.Products.UpdateTotalRatingAsync(productRating.ProductId))
                .MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineAutoFakeItEasyData(0)]
        [InlineAutoFakeItEasyData(1)]
        [InlineAutoFakeItEasyData(2)]
        [InlineAutoFakeItEasyData(3)]
        [InlineAutoFakeItEasyData(null)]
        [InlineAutoFakeItEasyData(4)]
        public async Task GivenParameters_WhenGetProductListAsync_ThenPaginatedProductListReturned(int? sortingParameter, int[] genreFilter, int[] ageFilter, int? pageNumber, int? pageSize, [Frozen] IUnitOfWork service, GamesService sut)
        {
            // Arrange
            var fixture = new Fixture { RepeatCount = 10 };
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var productList = fixture.Create<List<Product>>();

            A.CallTo(() => service.Products.GetListByAgeAndGenreFilterAsync(genreFilter, ageFilter))
                .Returns(productList);

            //Act
            var result = await sut.GetProductListAsync(sortingParameter, genreFilter, ageFilter, pageNumber,pageSize);

            //Assert
            Assert.IsAssignableFrom<PaginatedList<ReturnProductViewModel>>(result);
           
        }
    }
}
