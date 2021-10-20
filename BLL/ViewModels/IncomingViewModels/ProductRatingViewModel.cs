using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class ProductRatingViewModel
    {
        /// <summary>
        /// Product Id
        /// </summary>
        /// <example>0</example>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        /// <example>0</example>
        public int UserId { get; set; }

        /// <summary>
        /// Rating
        /// </summary>
        /// <example>0.0</example>
        [Required]
        public double Rating { get; set; }
    }
}
