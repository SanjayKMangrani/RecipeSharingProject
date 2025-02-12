// ViewRecipe.js
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

const ViewRecipe = () => {
  const { id } = useParams(); // Get the recipe id from the URL
  const [recipe, setRecipe] = useState(null);

  useEffect(() => {
    axios.get(`https://localhost:7172/api/recipes/${id}`)
      .then(response => setRecipe(response.data))
      .catch(error => console.error(error));
  }, [id]);

  if (!recipe) return <div>Loading...</div>;

  return (
    <div className="container mt-4">
      <h1>{recipe.title}</h1>
      <img src={recipe.imageUrl} alt={recipe.title} className="img-fluid" />
      <p><strong>Description:</strong> {recipe.description}</p>
      <p><strong>Ingredients:</strong></p>
      <pre>{recipe.ingredients}</pre>
      <p><strong>Instructions:</strong></p>
      <pre>{recipe.instructions}</pre>
    </div>
  );
};

export default ViewRecipe;
