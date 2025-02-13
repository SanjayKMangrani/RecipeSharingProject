Recipe Sharing - Full Stack Web Application

Recipe Sharing is a full-stack web application built with ASP.Net Core Web API, React JS, and SQL Server. This project allows users to register, log in, and share their favorite recipes. It includes a responsive front-end interface for managing recipes and a robust back-end API for handling user authentication and recipe CRUD operations.
Features

    User Authentication: Users can sign up, log in, and securely manage their sessions using JWT (JSON Web Tokens).
    Recipe Management: Users can create, view, and delete recipes.
    Recipe Sharing: Users can share their favorite recipes with the community, including ingredients, instructions, and images.
    Responsive Design: The front-end is built with React JS, providing a user-friendly interface across all devices.
    Postman API Testing: The backend API is fully tested using Postman for better development and documentation.

Technologies Used

    Backend:
        ASP.Net Core Web API for building the RESTful API
        Entity Framework Core for database operations
        JWT for user authentication
        SQL Server as the database

    Frontend:
        React JS for building the user interface
        Axios for making HTTP requests to the backend
        React Router for handling routing and navigation
        Bootstrap for responsive design

    Tools:
        Postman for testing and documenting the API

Installation and Setup

Follow these steps to set up and run the project locally.
1. Clone the repository

git clone https://github.com/YourUsername/RecipeSharingProject.git

2. Set up the Backend

    Navigate to the backend folder:

cd RecipeSharingProject/RecipeSharingBackend

Restore NuGet packages:

dotnet restore

Update the connection string in appsettings.json to point to your local SQL Server database.

Run the backend API:

    dotnet run

    The backend should now be running on http://localhost:5000.

3. Set up the Frontend

    Navigate to the frontend folder:

cd RecipeSharingProject/recipe-sharing-frontend

Install dependencies:

npm install

Run the frontend:

    npm start

    The frontend should now be running on http://localhost:3000.

4. Using the Application

    Sign Up and Log In: Create an account or log in using the Sign Up or Log In buttons.
    Create a Recipe: After logging in, you can create a new recipe by filling out the recipe form.
    View Recipes: You can view all recipes shared by others on the home page.
    My Recipes: View, edit, or delete the recipes that you have created.

API Endpoints
Authentication

    POST /api/auth/login: Logs in a user and returns a JWT token.
    POST /api/auth/register: Registers a new user.

Recipes

    GET /api/recipes: Retrieves all recipes.
    GET /api/recipes/my-recipes: Retrieves the current user's recipes.
    GET /api/recipes/{id}: Retrieves a specific recipe by ID.
    POST /api/recipes: Creates a new recipe (requires authentication).
    PUT /api/recipes/{id}: Updates a recipe by ID (requires authentication).
    DELETE /api/recipes/{id}: Deletes a recipe by ID (requires authentication).
