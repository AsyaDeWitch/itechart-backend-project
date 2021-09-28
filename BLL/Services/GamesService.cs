using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Data;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductDto _productDto;
        private readonly ProductRatingDto _productRatingDto;
        private readonly IMapper _mapper;
        private readonly IFirebaseService _firebaseService;

        public GamesService(ApplicationDbContext context, IMapper mapper, IFirebaseService firebaseService)
        {
            _context = context;
            _productDto = new ProductDto(_context);
            _productRatingDto = new ProductRatingDto(_context);
            _mapper = mapper;
            _firebaseService = firebaseService;
        }

        public async Task<Dictionary<string, int>> GetTopPlatformsAsync(int quantity)
        {
            List<(int, int)> resultList = await _productDto.GetTopPlatformsAsync();
            resultList.Sort((x, y) => y.Item2.CompareTo(x.Item2));

            Dictionary<string, int> resultDictionary = new();
            for (int i = 0; i < quantity; i++)
            {
                resultDictionary.Add(((Platform)resultList[i].Item1).ToString(), resultList[i].Item2);
            }
            return resultDictionary;
        }

        public async Task<List<ProductViewModel>> SearchGamesByNameAsync(string name)
        {
            var products = await _productDto.GetProductsByNameAsync(name);
            var resultProducts = new List<ProductViewModel>();
            for(int i = 0; i < products.Count; i++)
            {
                resultProducts.Add(_mapper.Map<ProductViewModel>(products[i]));
            }
            return resultProducts;
        }

        public async Task<List<ProductViewModel>> SearchGamesByParametersAsync(DateTime term, int limit, double offset, string name)
        {
            if (name == null)
            {
                name = "";
            }
            if (limit == 0)
            {
                var products = await _productDto.GetProductsByParametersWithoutLimitAsync(term, offset, name);
                var resultProducts = new List<ProductViewModel>();
                for (int i = 0; i < products.Count; i++)
                {
                    resultProducts.Add(_mapper.Map<ProductViewModel>(products[i]));
                }
                return resultProducts;
            }
            else
            {
                var products = await _productDto.GetProductsByParametersAsync(term, limit, offset, name);
                var resultProducts = new List<ProductViewModel>();
                for (int i = 0; i < products.Count; i++)
                {
                    resultProducts.Add(_mapper.Map<ProductViewModel>(products[i]));
                }
                return resultProducts;
            }
        }

        public async Task<ProductViewModel> GetProductFullInfoAsync(string id)
        {
            var product = await _productDto.GetProductFullInfoAsync(id);

            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<ProductViewModel> CreateProductAsync(ProductViewModel product)
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

            var createdProduct = await _productDto.CreateProductAsync(_mapper.Map<Product>(product));

            return _mapper.Map<ProductViewModel>(createdProduct);
        }

        public async Task<ProductViewModel> UpdateProductAsync(ProductViewModel product)
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

            var updatedProduct = await _productDto.UpdateProductAsync(_mapper.Map<Product>(product));
            return _mapper.Map<ProductViewModel>(updatedProduct);
        }

        public async Task DeleteProductByIdAsync(string id)
        {
            await _productDto.DeleteProductByIdAsync(id);
        }

        public async Task<ProductRatingViewModel> CreateProductRatingAsync(ProductRatingViewModel productRating)
        {
            var newProductRating = await _productRatingDto.CreateProductRatingAsync(_mapper.Map<ProductRating>(productRating));
            if (newProductRating != null)
            {
                await _productDto.UpdateProductTotalRatingAsync(productRating.ProductId);
            }
            return _mapper.Map<ProductRatingViewModel>(newProductRating);
        }

        public async Task<ProductRatingViewModel> UpdateProductRatingAsync(ProductRatingViewModel productRating)
        {
            if(productRating.Rating != 0)
            {
                var newProductRating = await _productRatingDto.UpdateProductRatingAsync(_mapper.Map<ProductRating>(productRating));
                if (newProductRating != null)
                {
                    await _productDto.UpdateProductTotalRatingAsync(productRating.ProductId);
                }
                return _mapper.Map<ProductRatingViewModel>(newProductRating);
            }
            return null;
        }

        public async Task DeleteProductRatingAsync(ProductRatingViewModel productRating)
        {
            await _productRatingDto.DeleteProductRatingAsync(_mapper.Map<ProductRating>(productRating));
            await _productDto.UpdateProductTotalRatingAsync(productRating.ProductId);
        }

        public async Task<PaginatedList<ProductViewModel>> GetProductListAsync(int? sortingParameter, int[] genreFilter, int[] ageFilter, int? pageNumber, int? pageSize)
        {
            var list = await _productDto.GetProductsByFiltersAsync(genreFilter, ageFilter);

            list = sortingParameter switch
            {
                (int)SortingParameterViewModel.Price_asc => list.OrderBy(p => p.Price).ToList(),
                (int)SortingParameterViewModel.Price_desc => list.OrderByDescending(p => p.Price).ToList(),
                (int)SortingParameterViewModel.TotalRating_asc => list.OrderBy(p => p.TotalRating).ToList(),
                (int)SortingParameterViewModel.TotalRating_desc => list.OrderByDescending(p => p.TotalRating).ToList(),
                _ => list.OrderBy(p => p.Name).ToList(),
            };

            var resultList = new List<ProductViewModel>();
            foreach(var product in list)
            {
                resultList.Add(_mapper.Map<ProductViewModel>(product));
            }
            return await PaginatedList<ProductViewModel>.CreateAsync(resultList, pageNumber ?? 1, pageSize ?? 10);
        }
    }
}
