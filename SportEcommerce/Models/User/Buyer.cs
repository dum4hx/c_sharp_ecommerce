using SportEcommerce.Models.Product;
using SportEcommerce.Models.Orders;
using System.ComponentModel.DataAnnotations;
namespace SportEcommerce.Models.User
{
    public class Buyer
    {
        // Primary key that references ApplicationUser.Id
        public required string Id { get; set; }

        [Required]
        [StringLength(500)]
        public string ShipmentAddress { get; set; }

        public required ApplicationUser User { get; set; }

        // Buyer orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        // Buyer cart products
        public ICollection<ProductType> CartProducts { get; set; } = new List<ProductType>();
    }
}
