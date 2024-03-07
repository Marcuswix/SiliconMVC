using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Model
{
    public class UserModel
    {
        //[Key]
        //public string Id { get; set; } = null!; 

        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        public string? UrlImage { get; set; } = null!;

        public string? Phone { get; set; } = null!;

        [StringLength(1000)]
        public string? Biography { get; set; } = null!;

        public int? AddressId { get; set; }

        public AddressModel? Address { get; set; }
    }
}
