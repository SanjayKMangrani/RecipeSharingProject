import React from 'react';
import { Link } from 'react-router-dom';

const RecipeItem = ({ recipe }) => {
  return (
    <div className="col-md-4 mb-4">
      <div className="card">
        <img src={recipe.imageUrl} className="card-img-top" alt={recipe.title} />
        <div className="card-body">
          <h5 className="card-title">{recipe.title}</h5>
          <p className="card-text">{recipe.description}</p>
          <Link to={`/recipe/${recipe.id}`} className="btn btn-primary">View Recipe</Link>
        </div>
      </div>
    </div>
  );
};

export default RecipeItem;