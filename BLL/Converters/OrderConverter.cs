using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using RIL.Models;

namespace BLL.Converters
{
    public class OrderConverter : IOrderConverter
    {
        private readonly IMapper _mapper;

        public OrderConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Order ConvertToOrder(OrderViewModel order)
        {
            return _mapper.Map<Order>(order);
        }

        public ReturnOrderViewModel ConvertToReturnOrderViewModel(Order order)
        {
            return _mapper.Map<ReturnOrderViewModel>(order);
        }
    }
}
