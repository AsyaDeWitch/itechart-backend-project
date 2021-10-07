using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class ReturnOrderViewModel
    {
        /// <summary>
        /// Order Id
        /// </summary>
        /// <example>12</example>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Order creation date
        /// </summary>
        /// <example>2013-12-31T00:00:00.00Z</example>
        [Required]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Products in order total amount
        /// </summary>
        /// <example>1</example>
        public int TotalAmount { get; set; }

        /// <summary>
        /// Order status
        /// </summary>
        /// <example>0</example>
        public string Status { get; set; }

        /// <summary>
        /// Order delivery type
        /// </summary>
        /// <example>0</example>
        [Required]
        public string DeliveryType { get; set; }

        /// <summary>
        /// Order addess delivery
        /// </summary>
        public AddressViewModel AddressDelivery { get; set; }
    }
}
