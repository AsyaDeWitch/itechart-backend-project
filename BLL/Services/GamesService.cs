using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Interfaces;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GamesService : IGamesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseService _firebaseService;
        private readonly IConverter _converter;

        public GamesService(IFirebaseService firebaseService, IUnitOfWork unitOfWork, IConverter converter)
        {
            _unitOfWork = unitOfWork;
            _firebaseService = firebaseService;
            _converter = converter;
        }

        public async Task<Dictionary<string, int>> GetTopPlatformsAsync(int quantity)
        {
            var resultList = await _unitOfWork.Products.GetEachPlatformCountAsync();
            resultList.Sort((x, y) => y.Item2.CompareTo(x.Item2));

            Dictionary<string, int> resultDictionary = new();
            for (var i = 0; i < quantity; i++)
            {
                resultDictionary.Add(((Platform)resultList[i].Item1).ToString(), resultList[i].Item2);
            }
            return resultDictionary;
        }

        public async Task<List<ReturnProductViewModel>> SearchGamesByParametersAsync(DateTime term, int limit, double offset, string name)
        {
            var products = await _unitOfWork.Products.GetProductsByParametersAsync(term, limit, offset, name);
            var resultProducts = new List<ReturnProductViewModel>();
            resultProducts.AddRange
            (products
                .Select(p => _converter.Product.ConvertToReturnProductViewModel(p)));
            return resultProducts;
        }

        public async Task<ReturnProductViewModel> GetProductFullInfoAsync(string id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(int.Parse(id));
            return _converter.Product.ConvertToReturnProductViewModel(product);
        }

        public async Task<ReturnProductViewModel> CreateProductAsync(ProductViewModel product)
        {
            if (product.LogoImageFile != null)
            {
                var linkToImage = await _firebaseService.UploadLogoImageAsync(product.LogoImageFile);
                if (!string.IsNullOrWhiteSpace(linkToImage))
                {
                    product.Logo = linkToImage;
                }
            }
            if (product.BackgroundImageFile != null)
            {
                var linkToImage = await _firebaseService.UploadBackgroundImageAsync(product.BackgroundImageFile);
                if (!string.IsNullOrWhiteSpace(linkToImage))
                {
                    product.Background = linkToImage;
                }
            }

            var createdProduct = await _unitOfWork.Products.CreateAsync(_converter.Product.ConvertToProduct(product));
            return _converter.Product.ConvertToReturnProductViewModel(createdProduct);
        }

        public async Task<ReturnProductViewModel> UpdateProductAsync(ProductViewModel product)
        {
            if(product.Id == 0)
            {
                return null;
            }

            if (product.LogoImageFile != null)
            {
                var linkToImage = await _firebaseService.UploadLogoImageAsync(product.LogoImageFile);
                if (!string.IsNullOrWhiteSpace(linkToImage))
                {
                    product.Logo = linkToImage;
                }
            }

            if (product.BackgroundImageFile != null)
            {
                var linkToImage = await _firebaseService.UploadBackgroundImageAsync(product.BackgroundImageFile);
                if (!string.IsNullOrWhiteSpace(linkToImage))
                {
                    product.Background = linkToImage;
                }
            }

            var updatedProduct = await _unitOfWork.Products.UpdateAsync(_converter.Product.ConvertToProduct(product));
            return _converter.Product.ConvertToReturnProductViewModel(updatedProduct);
        }

        public async Task DeleteProductByIdAsync(string id)
        {
            await _unitOfWork.Products.DeleteByIdAsync(int.Parse(id));
        }

        public async Task<ProductRatingViewModel> CreateProductRatingAsync(ProductRatingViewModel productRating)
        {
            var newProductRating = await _unitOfWork.ProductRatings.CreateAsync(_converter.ProductRating.ConvertToProductRating(productRating));
            if (newProductRating != null)
            {
                await _unitOfWork.Products.UpdateTotalRatingAsync(productRating.ProductId);
            }

            return _converter.ProductRating.ConvertToProductRatingViewModel(newProductRating);
        }

        public async Task<ProductRatingViewModel> UpdateProductRatingAsync(ProductRatingViewModel productRating)
        {
            if(productRating.Rating == 0)
            {
                return null;
            }
            var newProductRating = await _unitOfWork.ProductRatings.UpdateAsync(_converter.ProductRating.ConvertToProductRating(productRating));
            if (newProductRating != null)
            {
                await _unitOfWork.Products.UpdateTotalRatingAsync(productRating.ProductId);
            }
            return _converter.ProductRating.ConvertToProductRatingViewModel(newProductRating);
        }

        public async Task DeleteProductRatingAsync(ProductRatingViewModel productRating)
        {
            await _unitOfWork.ProductRatings.DeleteAsync(_converter.ProductRating.ConvertToProductRating(productRating));
            await _unitOfWork.Products.UpdateTotalRatingAsync(productRating.ProductId);
        }

        public async Task<PaginatedList<ReturnProductViewModel>> GetProductListAsync(int? sortingParameter, int[] genreFilter, int[] ageFilter, int? pageNumber, int? pageSize)
        {
            var list = await _unitOfWork.Products.GetListByAgeAndGenreFilterAsync(genreFilter, ageFilter);

            list = sortingParameter switch
            {
                (int)SortingParameterViewModel.PriceAsc => list.OrderBy(p => p.Price).ToList(),
                (int)SortingParameterViewModel.PriceDesc => list.OrderByDescending(p => p.Price).ToList(),
                (int)SortingParameterViewModel.TotalRatingAsc => list.OrderBy(p => p.TotalRating).ToList(),
                (int)SortingParameterViewModel.TotalRatingDesc => list.OrderByDescending(p => p.TotalRating).ToList(),
                _ => list.OrderBy(p => p.Name).ToList(),
            };

            var resultList = new List<ReturnProductViewModel>();
            resultList.AddRange
            (list
                .Select(p => _converter.Product.ConvertToReturnProductViewModel(p)));
            return await PaginatedList<ReturnProductViewModel>.CreateAsync(resultList, pageNumber ?? 1, pageSize ?? 10);
        }
    }
}
