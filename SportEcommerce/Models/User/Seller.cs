using SportEcommerce.Models.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEcommerce.Models.User
{
    public class Seller
    {
        // Primary key that references ApplicationUser.Id
        public required string Id { get; set; }
        
        public required ApplicationUser User { get; set; }

        // Store data
        [Column(TypeName = "varchar(150)")]
        public string StoreName { get; set; } = "Anonymous";

        [Column(TypeName = "varchar(500)")]
        public string? Description { get; set; }
        
        // Seller's products
        public ICollection<ProductType> ProductTypes = new List<ProductType>();
    }
}
