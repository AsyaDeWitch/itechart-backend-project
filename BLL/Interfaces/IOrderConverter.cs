using BLL.ViewModels;
using RIL.Models;

namespace BLL.Interfaces
{
    public interface IOrderConverter
    {
        public Order ConvertToOrder(OrderViewModel order);
        public ReturnOrderViewModel ConvertToReturnOrderViewModel(Order order);
    }
}
