using BLL.ViewModels;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using RIL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DIL.ActionFilters
{
    public class OrderAndProductsValidationActionFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderAndProductsValidationActionFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                        if (await _unitOfWork.Products.IsContainedInDbAsync(product.ProductId))
                        {
                            var count = await _unitOfWork.Products.GetCountByIdAsync(product.ProductId);
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
                    order.DeliveryType = (int)DeliveryType.OnDemandDelivery;
                }
                if (order.Status > Enum.GetValues(typeof(OrderStatus)).Cast<int>().Last()
                    || order.Status < Enum.GetValues(typeof(OrderStatus)).Cast<int>().First())
                {
                    order.Status = (int)OrderStatus.AwaitingPayment;
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
