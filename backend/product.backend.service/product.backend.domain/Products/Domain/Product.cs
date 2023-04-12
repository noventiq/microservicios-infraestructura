//using FluentValidation;

namespace product.backend.domain.Products.Domain
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal discountPercentage { get; set; }
        public decimal rating { get; set; }
        public int stock { get; set; }
        public string brand { get; set; }
        public string category { get; set; }
        public DateTime? createdAt { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedAt { get; set; }
        public string updatedBy { get; set; }
    }
}
