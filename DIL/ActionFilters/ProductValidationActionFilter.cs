using BLL.ViewModels;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace DIL.ActionFilters
{
    public class ProductValidationActionFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductValidationActionFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var products = context.ActionArguments["products"] as ProductOrderViewModel[];
            if (products.Length != 0)
            {
                foreach(var product in products)
                {
                    if (await _unitOfWork.Products.IsContainedInDbAsync(product.ProductId))
                    {
                        var count = await _unitOfWork.Products.GetCountByIdAsync(product.ProductId);
                        if(product.ProductAmount > count)
                        {
                            product.ProductAmount = count;
                        }
                    }
                    else
                    {
                        products = products.Where(p => p != product).ToArray();
                    }
                }
            }
            var result = await next();
        }
    }
}
