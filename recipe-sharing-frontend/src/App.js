import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';
import RecipeList from './components/RecipeList';
import RecipeForm from './components/RecipeForm';
import Login from './components/Login';
import Register from './components/Register';
import ViewRecipe from './components/ViewRecipe';
import MyRecipes from './components/MyRecipes';
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <AuthProvider>
      <Router>
        <Navbar />
        <div className="container mt-4">
          <Routes>
            <Route path="/" element={<RecipeList />} />
            <Route path="/add-recipe" element={<RecipeForm />} />
            <Route path="/recipe/:id" element={<ViewRecipe />} />
            <Route path="/my-recipes" element={<MyRecipes />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/add-recipe" element={<ProtectedRoute><RecipeForm /></ProtectedRoute>}/>
          </Routes>
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;