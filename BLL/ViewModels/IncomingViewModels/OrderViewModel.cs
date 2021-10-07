using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class OrderViewModel
    {
        /// <summary>
        /// Order Id
        /// </summary>
        /// <example>12</example>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Products in order total amount
        /// </summary>
        /// <example>1</example>
        public int TotalAmount { get; set; }

        /// <summary>
        /// Order status
        /// </summary>
        /// <example>0</example>
        public int Status { get; set; }

        /// <summary>
        /// Order delivery type
        /// </summary>
        /// <example>0</example>
        [Required]
        public int DeliveryType { get; set; }

        /// <summary>
        /// Order addess delivery
        /// </summary>
        public AddressViewModel AddressDelivery { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        /// <example>1</example>
        public int UserId { get; set; }
    }
}
