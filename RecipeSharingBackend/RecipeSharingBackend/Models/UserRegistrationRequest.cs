﻿using System.ComponentModel.DataAnnotations;

namespace RecipeSharingBackend.Models
{
    public class UserRegistrationRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
