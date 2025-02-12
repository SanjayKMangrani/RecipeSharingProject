import React, { createContext, useState, useEffect } from 'react';

const AuthContext = createContext();

const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  // Check if the user is already logged in (e.g., from localStorage)
  useEffect(() => {
    const storedUser = JSON.parse(localStorage.getItem('user'));
    if (storedUser && isTokenValid(storedUser.token)) {
      setUser(storedUser);
    } else {
      localStorage.removeItem('user'); // Remove invalid token if expired
    }
  }, []);

  // Helper function to check if token is expired
  const isTokenValid = (token) => {
    try {
      const decoded = JSON.parse(atob(token.split('.')[1])); // Decode the JWT
      const expiry = decoded.exp;
      return Date.now() < expiry * 1000; // Check if token is expired
    } catch (e) {
      return false;
    }
  };

  const login = (userData) => {
    const user = {
      username: userData.username,
      token: userData.token,
    };
    setUser(user);
    localStorage.setItem('user', JSON.stringify(user)); // Save user data in localStorage
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem('user'); // Remove user data from localStorage
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export { AuthContext, AuthProvider };
