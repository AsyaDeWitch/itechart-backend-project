namespace RIL.Models
{
    public class ProductRating
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public ExtendedUser User { get; set; }
        public double Rating { get; set; }
    }
}
