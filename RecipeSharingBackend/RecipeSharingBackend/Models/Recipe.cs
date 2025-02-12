using RecipeSharingBackend.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public string ImageUrl { get; set; }

    // Foreign key to User
    public int UserId { get; set; }
    public User User { get; set; } // Navigation property
}
