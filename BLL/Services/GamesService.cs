using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IFirebaseService _firebaseService;

        public GamesService(IMapper mapper, IFirebaseService firebaseService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebaseService = firebaseService;
        }

        public async Task<Dictionary<string, int>> GetTopPlatformsAsync(int quantity)
        {
            List<(int, int)> resultList = await _unitOfWork.Products.GetEachPlatformCountAsync();
            resultList.Sort((x, y) => y.Item2.CompareTo(x.Item2));

            Dictionary<string, int> resultDictionary = new();
            for (int i = 0; i < quantity; i++)
            {
                resultDictionary.Add(((Platform)resultList[i].Item1).ToString(), resultList[i].Item2);
            }
            return resultDictionary;
        }

        public async Task<List<ReturnProductViewModel>> SearchGamesByParametersAsync(DateTime term, int limit, double offset, string name)
        {
            var products = await _unitOfWork.Products.GetProductsByParametersAsync(term, limit, offset, name);
            var resultProducts = new List<ReturnProductViewModel>();
            for (int i = 0; i < products.Count; i++)
            {
                resultProducts.Add(_mapper.Map<ReturnProductViewModel>(products[i]));
            }
            return resultProducts;
        }

        public async Task<ReturnProductViewModel> GetProductFullInfoAsync(string id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(int.Parse(id));
            return _mapper.Map<ReturnProductViewModel>(product);
        }

        public async Task<ReturnProductViewModel> CreateProductAsync(ProductViewModel product)
        {
            if (product.LogoImageFile != null)
            {
                var linkToImage = await _firebaseService.UploadLogoImageAsync(product.LogoImageFile);
                if (!String.IsNullOrWhiteSpace(linkToImage))
                {
                    product.Logo = linkToImage;
                }
            }
            if (product.BackgroundImageFile != null)
            {
                var linkToImage = await _firebaseService.UploadBackgroundImageAsync(product.BackgroundImageFile);
                if (!String.IsNullOrWhiteSpace(linkToImage))
                {
                    product.Background = linkToImage;
                }
            }

            var createdProduct = await _unitOfWork.Products.CreateAsync(_mapper.Map<Product>(product));
            return _mapper.Map<ReturnProductViewModel>(createdProduct);
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
                if (!String.IsNullOrWhiteSpace(linkToImage))
                {
                    product.Logo = linkToImage;
                }
            }

            if (product.BackgroundImageFile != null)
            {
                var linkToImage = await _firebaseService.UploadBackgroundImageAsync(product.BackgroundImageFile);
                if (!String.IsNullOrWhiteSpace(linkToImage))
                {
                    product.Background = linkToImage;
                }
            }

            var updatedProduct = await _unitOfWork.Products.UpdateAsync(_mapper.Map<Product>(product));
            return _mapper.Map<ReturnProductViewModel>(updatedProduct);
        }

        public async Task DeleteProductByIdAsync(string id)
        {
            await _unitOfWork.Products.DeleteByIdAsync(int.Parse(id));
        }

        public async Task<ProductRatingViewModel> CreateProductRatingAsync(ProductRatingViewModel productRating)
        {
            var newProductRating = await _unitOfWork.ProductRatings.CreateAsync(_mapper.Map<ProductRating>(productRating));
            if (newProductRating != null)
            {
                await _unitOfWork.Products.UpdateTotalRatingAsync(productRating.ProductId);
            }
            return _mapper.Map<ProductRatingViewModel>(newProductRating);
        }

        public async Task<ProductRatingViewModel> UpdateProductRatingAsync(ProductRatingViewModel productRating)
        {
            if(productRating.Rating != 0)
            {
                var newProductRating = await _unitOfWork.ProductRatings.UpdateAsync(_mapper.Map<ProductRating>(productRating));
                if (newProductRating != null)
                {
                    await _unitOfWork.Products.UpdateTotalRatingAsync(productRating.ProductId);
                }
                return _mapper.Map<ProductRatingViewModel>(newProductRating);
            }
            return null;
        }

        public async Task DeleteProductRatingAsync(ProductRatingViewModel productRating)
        {
            await _unitOfWork.ProductRatings.DeleteAsync(_mapper.Map<ProductRating>(productRating));
            await _unitOfWork.Products.UpdateTotalRatingAsync(productRating.ProductId);
        }

        public async Task<PaginatedList<ReturnProductViewModel>> GetProductListAsync(int? sortingParameter, int[] genreFilter, int[] ageFilter, int? pageNumber, int? pageSize)
        {
            var list = await _unitOfWork.Products.GetListByAgeAndGenreFilterAsync(genreFilter, ageFilter);

            list = sortingParameter switch
            {
                (int)SortingParameterViewModel.Price_asc => list.OrderBy(p => p.Price).ToList(),
                (int)SortingParameterViewModel.Price_desc => list.OrderByDescending(p => p.Price).ToList(),
                (int)SortingParameterViewModel.TotalRating_asc => list.OrderBy(p => p.TotalRating).ToList(),
                (int)SortingParameterViewModel.TotalRating_desc => list.OrderByDescending(p => p.TotalRating).ToList(),
                _ => list.OrderBy(p => p.Name).ToList(),
            };

            var resultList = new List<ReturnProductViewModel>();
            foreach(var product in list)
            {
                resultList.Add(_mapper.Map<ReturnProductViewModel>(product));
            }
            return await PaginatedList<ReturnProductViewModel>.CreateAsync(resultList, pageNumber ?? 1, pageSize ?? 10);
        }
    }
}
