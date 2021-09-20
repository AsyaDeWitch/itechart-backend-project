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
            CreateMap<Product, ProductViewModel>();
        }
    }
}
