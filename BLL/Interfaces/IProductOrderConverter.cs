using BLL.ViewModels;
using RIL.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IProductOrderConverter
    {
        public List<ProductOrder> ConvertToProductOrderList(ProductOrderViewModel[] products, Order order);
        public List<ProductOrderViewModel> ConvertToProductOrderViewModelList(List<ProductOrder> productOrders);
        public List<ProductOrder> ConvertToProductOrderList(ProductOrderViewModel[] products);
        public ProductOrderViewModel ConvertToProductOrderViewModel(ProductOrder productOrder);
    }
}
