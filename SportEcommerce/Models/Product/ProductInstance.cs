using System.ComponentModel.DataAnnotations.Schema;

namespace SportEcommerce.Models.Product
{
    public class ProductInstance
    {
        public int Id { get; set; }

        // Status of the product instance
        public required string Status { get; set; }

        // Product type
        public required int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public required ProductType ProductType { get; set; }
    }
}
