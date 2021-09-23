using AutoMapper;
using BLL.ViewModels;
using RIL.Models;

namespace DIL.Settings
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            CreateMap<Address, AddressViewModel>();
            CreateMap<AddressViewModel, Address>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();

            CreateMap<ProductRating, ProductRatingViewModel>();
            CreateMap<ProductRatingViewModel, ProductRating>();
        }
    }
}
