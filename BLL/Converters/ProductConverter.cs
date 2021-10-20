using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using RIL.Models;

namespace BLL.Converters
{
    public class ProductConverter : IProductConverter
    {

        private readonly IMapper _mapper;

        public ProductConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ReturnProductViewModel ConvertToReturnProductViewModel(Product product)
        {
            return _mapper.Map<ReturnProductViewModel>(product);
        }

        public Product ConvertToProduct(ProductViewModel product)
        {
            return _mapper.Map<Product>(product);
        }
    }
}
