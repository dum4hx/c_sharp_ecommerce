using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEcommerce.Models.User
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(60)")]
        [Display(Name = "Country")]
        public required string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
