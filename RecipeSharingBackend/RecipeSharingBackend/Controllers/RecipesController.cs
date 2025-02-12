using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeSharingBackend.Data;
using RecipeSharingBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace RecipeSharingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeContext _context;

        public RecipesController(RecipeContext context)
        {
            _context = context;
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        // GET: api/recipes/my-recipes
        [HttpGet("my-recipes")]
        [Authorize] // Ensure user is authenticated
        public async Task<ActionResult<IEnumerable<Recipe>>> GetMyRecipes()
        {
            var userId = GetUserIdFromToken(); // Extract user ID from JWT token

            if (userId == 0)
            {
                return Unauthorized(); // Return Unauthorized if no valid user ID found
            }

            var recipes = await _context.Recipes
                .Where(r => r.UserId == userId) // Filter recipes by the logged-in user's ID
                .ToListAsync();

            return Ok(recipes);
        }

        // GET: api/recipes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
                return NotFound();

            return recipe;
        }

        // POST: api/recipes
        [HttpPost]
        [Authorize] // Ensure user is authenticated
        public async Task<ActionResult<Recipe>> CreateRecipe(Recipe recipe)
        {
            var userId = GetUserIdFromToken(); // Get user ID from JWT token

            if (userId == 0)
            {
                return Unauthorized(); // Return Unauthorized if no valid user ID found
            }

            recipe.UserId = userId; // Associate the recipe with the logged-in user

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        // PUT: api/recipes/{id}
        [HttpPut("{id}")]
        [Authorize] // Ensure user is authenticated
        public async Task<IActionResult> UpdateRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id) return BadRequest();

            var userId = GetUserIdFromToken(); // Get user ID from JWT token

            if (userId == 0 || recipe.UserId != userId)
            {
                return Unauthorized(); // Ensure user is updating their own recipe
            }

            _context.Entry(recipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/recipes/{id}
        [HttpDelete("{id}")]
        [Authorize] // Ensure user is authenticated
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
                return NotFound();

            var userId = GetUserIdFromToken(); // Get user ID from JWT token

            if (userId == 0 || recipe.UserId != userId)
            {
                return Unauthorized(); // Ensure user is deleting their own recipe
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to extract user ID from JWT token
        private int GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId"); // "userId" should match the claim name in the token
            if (userIdClaim != null)
            {
                return int.Parse(userIdClaim.Value); // Parse and return the User ID
            }
            return 0; // Return 0 if user ID is not found in the token
        }
    }
}
