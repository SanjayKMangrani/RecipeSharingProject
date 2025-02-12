import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './RecipeList.css'; // Add custom CSS

const RecipeList = () => {
  const [recipes, setRecipes] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:7172/api/recipes')
      .then(response => setRecipes(response.data))
      .catch(error => console.error(error));
  }, []);

  return (
    <div className="recipe-list-container">
      <h1 className="recipe-list-title">Recipes</h1>
      <div className="row">
        {recipes.map(recipe => (
          <div key={recipe.id} className="col-md-4 mb-4">
            <div className="recipe-card">
              <img src={recipe.imageUrl} alt={recipe.title} className="recipe-image" />
              <div className="recipe-card-body">
                <h5 className="recipe-title">{recipe.title}</h5>
                <p className="recipe-description">{recipe.description}</p>
                <Link to={`/recipe/${recipe.id}`} className="btn btn-primary">View Recipe</Link>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default RecipeList;