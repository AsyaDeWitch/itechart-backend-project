using BLL.ViewModels;
using DAL.Data;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using RIL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DIL.ActionFilters
{
    public class OrderAndProductsValidationActionFilter : IAsyncActionFilter
    {
        private readonly ProductRepository _productRepository;
        private readonly ApplicationDbContext _context;

        public OrderAndProductsValidationActionFilter(ApplicationDbContext context)
        {
            _context = context;
            _productRepository = new ProductRepository(_context);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context.ActionArguments.ContainsKey("orderProducts"))
            {
                var orderProducts = context.ActionArguments["orderProducts"] as OrderProductsViewModel;

                var products = orderProducts.Products;
                if (products.Length != 0)
                {
                    foreach (var product in products)
                    {
                        if (await _productRepository.IsContainedInDb(product.ProductId))
                        {
                            var count = await _productRepository.GetProductCount(product.ProductId);
                            if (product.ProductAmount > count)
                            {
                                product.ProductAmount = count;
                            }
                            if (product.ProductAmount < 0)
                            {
                                product.ProductAmount = 0;
                            }
                        }
                        else
                        {
                            products = products.Where(p => p != product).ToArray();
                        }
                    }
                }

                var order = orderProducts.Order;
                if (order.DeliveryType > Enum.GetValues(typeof(DeliveryType)).Cast<int>().Last()
                    || order.DeliveryType < Enum.GetValues(typeof(DeliveryType)).Cast<int>().First())
                {
                    order.DeliveryType = (int)DeliveryType.On_Demand_Delivery;
                }
                if (order.Status > Enum.GetValues(typeof(OrderStatus)).Cast<int>().Last()
                    || order.Status < Enum.GetValues(typeof(OrderStatus)).Cast<int>().First())
                {
                    order.Status = (int)OrderStatus.Awaiting_Payment;
                }
                context.ActionArguments["orderProducts"] = new OrderProductsViewModel
                {
                    Products = products,
                    Order = order,
                };
            }
            var result = await next();
        }
    }
}
