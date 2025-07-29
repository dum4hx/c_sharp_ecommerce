using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SportEcommerce.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "varchar(40)")]
        public required string FirstName { get; set; }

        [Column(TypeName = "varchar(40)")]
        public string? LastName { get; set; }

        [Column(TypeName = "varchar(40)")]
        public required string FirstSurname { get; set; }

        [Column(TypeName = "varchar(40)")]
        public string? LastSurname { get; set; }

        public int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country? Country { get; set; }    

        // Role mappers
        public Seller? Seller { get; set; }
        public Buyer? Buyer { get; set; }
    }
}
