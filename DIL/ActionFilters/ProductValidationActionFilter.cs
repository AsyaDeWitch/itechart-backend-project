using BLL.ViewModels;
using DAL.Data;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace DIL.ActionFilters
{
    public class ProductValidationActionFilter : IAsyncActionFilter
    {
        private readonly ProductRepository _productRepository;

        public ProductValidationActionFilter(ApplicationDbContext context)
        {
            _productRepository = new ProductRepository(context);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var products = context.ActionArguments["products"] as ProductOrderViewModel[];
            if (products.Length != 0)
            {
                foreach(var product in products)
                {
                    if (await _productRepository.IsContainedInDb(product.ProductId))
                    {
                        var count = await _productRepository.GetProductCount(product.ProductId);
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
