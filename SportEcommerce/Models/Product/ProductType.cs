using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SportEcommerce.Models.User;

namespace SportEcommerce.Models.Product
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(150)")]
        public required string Name { get; set; }

        [Column(TypeName = "float(6,2)")]
        [Display(Name = "Price")]
        public required float Price { get; set; }

        [Column(TypeName = "varchar(1000)")]
        public required string Description { get; set; }

        // Product image
        [DataType(DataType.ImageUrl)]
        [Column(TypeName = "varchar(500)")]
        public string? ImageUrl { get; set; }

        // Product brand
        public int? BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }

        // Product related seller
        [Column(TypeName = "varchar(36)")]
        public required string SellerId { get; set; }

        [ForeignKey("SellerId")]
        public required Seller Seller { get; set; }

        // Mapper to product instances
        public ICollection<ProductInstance> ProductInstances { get; set; } = new List<ProductInstance>();


    }
}
