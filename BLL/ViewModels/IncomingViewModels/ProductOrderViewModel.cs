using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class ProductOrderViewModel
    {
        /// <summary>
        /// Product Id
        /// </summary>
        /// <example>1</example>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Amount of products in order
        /// </summary>
        /// <example>3</example>
        [Required]
        public int ProductAmount { get; set; }
    }
}
