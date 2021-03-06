using AutoMapper;
using BLL.ViewModels;
using RIL.ModelExtensions;
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

            CreateMap<ProductViewModel, ReturnProductViewModel>()
                .ForMember("Genre", opt => opt.MapFrom(c => ((Genre)c.Genre).ToDescriptionString()))
                .ForMember("Platform", opt => opt.MapFrom(c => ((Platform)c.Platform).ToDescriptionString()))
                .ForMember("Rating", opt => opt.MapFrom(c => ((Rating)c.Rating).ToDescriptionString()));

            CreateMap<Product, ReturnProductViewModel>()
                .ForMember("Genre", opt => opt.MapFrom(c => ((Genre)c.Genre).ToDescriptionString()))
                .ForMember("Platform", opt => opt.MapFrom(c => ((Platform)c.Platform).ToDescriptionString()))
                .ForMember("Rating", opt => opt.MapFrom(c => ((Rating)c.Rating).ToDescriptionString()));

            CreateMap<ProductOrder, ProductOrderViewModel>();
            CreateMap<ProductOrderViewModel, ProductOrder>();

            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderViewModel, Order>();

            CreateMap<Order, ReturnOrderViewModel>()
                .ForMember("Status", opt => opt.MapFrom(c => ((OrderStatus)c.Status).ToDescriptionString()))
                .ForMember("DeliveryType", opt => opt.MapFrom(c => ((DeliveryType)c.DeliveryType).ToDescriptionString()));

            CreateMap<OrderViewModel, ReturnOrderViewModel>()
                .ForMember("Status", opt => opt.MapFrom(c => ((OrderStatus)c.Status).ToDescriptionString()))
                .ForMember("DeliveryType", opt => opt.MapFrom(c => ((DeliveryType)c.DeliveryType).ToDescriptionString()));

            CreateMap<ProductOrder, ProductOrderViewModel>();
            CreateMap<ProductOrderViewModel, ProductOrder>();
        }
    }
}
