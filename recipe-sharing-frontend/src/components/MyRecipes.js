import React, { useEffect, useState, useContext } from 'react';
import axios from 'axios';
import { AuthContext } from '../context/AuthContext'; // Import AuthContext
import { useNavigate } from 'react-router-dom'; // Navigate to redirect to login if not logged in


const MyRecipes = () => {
  const { user } = useContext(AuthContext); // Get user data from context
  const [recipes, setRecipes] = useState([]);
  const navigate = useNavigate(); // For redirecting if the user is not logged in

  useEffect(() => {
    if (!user || !user.token) {
      // If user is not logged in or token is not available, redirect to login
      navigate('/login');
      return;
    }

    // Make API request to get the user's recipes
    axios
      .get('https://localhost:7172/api/recipes/my-recipes', {
        headers: { Authorization: `Bearer ${user.token}` }, // Include JWT token in headers
      })
      .then((response) => {
        setRecipes(response.data); // Set the recipes data from API
      })
      .catch((error) => {
        console.error('Error fetching recipes:', error);
        if (error.response && error.response.status === 401) {
          // Handle 401 Unauthorized: User might need to log in again
          navigate('/login'); // Redirect to login if the token is invalid or expired
        }
      });
  }, [user, navigate]); // Only fetch recipes if user is logged in

  return (
    <div className="container mt-4">
      <h1>My Recipes</h1>
      {recipes.length > 0 ? (
        <div className="row">
          {recipes.map((recipe) => (
            <div key={recipe.id} className="col-md-4 mb-4">
              <div className="recipe-card">
                <img
                  src={recipe.imageUrl}
                  alt={recipe.title}
                  className="recipe-image"
                />
                <div className="recipe-card-body">
                  <h5 className="recipe-title">{recipe.title}</h5>
                  <p className="recipe-description">{recipe.description}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      ) : (
        <p>No recipes found</p>
      )}
    </div>
  );
};

export default MyRecipes;
