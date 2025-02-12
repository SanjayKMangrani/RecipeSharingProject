using System.ComponentModel.DataAnnotations;

namespace RecipeSharingBackend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } 

        public string PasswordHash { get; set; } 

        public int RoleId { get; set; } 
        public Role Role { get; set; } 
    }
}