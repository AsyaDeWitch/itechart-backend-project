using BLL.ViewModels;
using RIL.Models;

namespace BLL.Interfaces
{
    public interface IProductConverter
    {
        public ReturnProductViewModel ConvertToReturnProductViewModel(Product product);
        public Product ConvertToProduct(ProductViewModel product);
    }
}
