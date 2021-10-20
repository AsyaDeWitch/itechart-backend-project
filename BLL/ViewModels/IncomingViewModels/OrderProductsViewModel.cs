namespace BLL.ViewModels
{
    public class OrderProductsViewModel
    {
        /// <summary>
        /// Order
        /// </summary>
        public OrderViewModel Order { get; set; }

        /// <summary>
        /// List of products including products from order and their amount
        /// </summary>
        public ProductOrderViewModel[] Products { get; set; }
    }
}
