using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class UserEntity : IdentityUser
    {
        [Required]
        [StringLength(50)]
        [ProtectedPersonalData]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(50)]
        [ProtectedPersonalData]
        public string LastName { get; set; } = null!;

        [StringLength(1000)]
        public string? Biography { get; set; } = null!;

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public int? AddressId { get; set; }

        public AddressEntity? Address { get; set; }
    }
}
