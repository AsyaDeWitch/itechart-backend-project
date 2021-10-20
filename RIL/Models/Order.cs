using System;
using System.Collections.Generic;

namespace RIL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int TotalAmount { get; set; }
        public int Status { get; set; }
        public int DeliveryType { get; set; }
        public int? AddressDeliveryId { get; set; }
        public Address AddressDelivery { get; set; }
        public ICollection<Product> Products { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
        public int UserId { get; set; }
        public ExtendedUser User { get; set; }
    }
}
