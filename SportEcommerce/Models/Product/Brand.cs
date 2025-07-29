using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEcommerce.Models.Product
{
    public class Brand
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public required string Name { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime? FoundationDate { get; set; }

        public ICollection<ProductType> ProductTypes { get; set; } = new List<ProductType>();
    }
}
