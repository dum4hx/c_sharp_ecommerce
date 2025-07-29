using SportEcommerce.Models.Product;
using SportEcommerce.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEcommerce.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }
        
        // Order date
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        // Buyer who placed the order
        public required string BuyerId { get; set; }
        
        [ForeignKey("BuyerId")]
        public required Buyer Buyer { get; set; }
        
        // Products in the order
        public ICollection<ProductInstance> Products { get; set; } = new List<ProductInstance>();
        
        // Total price of the order
        [Column(TypeName = "float(6,2)")]
        public float TotalPrice { get; set; }
    }
}
