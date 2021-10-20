using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using RIL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Converters
{
    public class ProductOrderConverter : IProductOrderConverter
    {
        private readonly IMapper _mapper;

        public ProductOrderConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<ProductOrder> ConvertToProductOrderList(ProductOrderViewModel[] products, Order order)
        {
            var productOrders = new List<ProductOrder>();
            foreach (var product in products)
            {
                var productOrder = _mapper.Map<ProductOrder>(product);
                productOrder.Order = order;
                productOrders.Add(productOrder);
            }

            return productOrders;
        }

        public List<ProductOrder> ConvertToProductOrderList(ProductOrderViewModel[] products)
        {
            var productOrders = new List<ProductOrder>();
            productOrders.AddRange
            (products
                .Select(p => _mapper.Map<ProductOrder>(p)));
            return productOrders;
        }

        public ProductOrderViewModel ConvertToProductOrderViewModel(ProductOrder productOrder)
        {
            return _mapper.Map<ProductOrderViewModel>(productOrder);
        }

        public List<ProductOrderViewModel> ConvertToProductOrderViewModelList(List<ProductOrder> productOrders)
        {
            var productList = new List<ProductOrderViewModel>();
            productList.AddRange
            (productOrders
                .Select(p => _mapper.Map<ProductOrderViewModel>(p)));
            return productList;
        }
    }
}
