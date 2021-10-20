using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class ReturnProductOrderViewModel
    {
        /// <summary>
        /// Order
        /// </summary>
        [Required]
        public ReturnOrderViewModel ReturnOrderViewModel { get; set; }

        /// <summary>
        /// Products list in order
        /// </summary>
        [Required]
        public List<ProductOrderViewModel> ProductOrderViewModels { get; set; }
    }
}
