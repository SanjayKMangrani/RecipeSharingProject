using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using RecipeSharingBackend.Models;
using RecipeSharingBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace RecipeSharingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RecipeContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(RecipeContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegistrationRequest request)
        {
            // Check if the username or email already exists
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return BadRequest("Username already exists.");
            }

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("Email already exists.");
            }

            // Create a new User object
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password, // Plain-text password
                PasswordHash = HashPassword(request.Password), // Hashed password
                RoleId = 1 // Default role ID for "User"
            };

            // Save the user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest request)
        {
            // Find the user by username
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Verify the password (you should use a proper hashing library like BCrypt)
            if (!VerifyPassword(request.Password, user.PasswordHash))
            {
                return BadRequest("Invalid password.");
            }

            // Generate a JWT token
            var token = GenerateJwtToken(user);

            return Ok(new { username = user.Username, token });
        }

        // Helper method to hash passwords (use a proper library like BCrypt in production)
        private string HashPassword(string password)
        {
            return password; // Replace with proper hashing logic
        }

        // Helper method to verify passwords (use a proper library like BCrypt in production)
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return password == hashedPassword; // Replace with proper verification logic
        }

        // Helper method to generate a JWT token
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // Login request model
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}