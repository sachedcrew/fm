import axios from 'axios';

const API_URL = 'https://localhost:7049/';

const register = (email, password) => {
    return axios.post(API_URL + 'register', {
        email,
        password
    });
};

const login = (email, password) => {
    return axios.post(API_URL + 'login', { email, password }, { withCredentials: true })
        .then(response => {
            if (response.status === 200) {
                localStorage.setItem('user', JSON.stringify({ email }));
            }
            return response.data;
        });
};

const logout = () => {
    return axios.post(API_URL + 'logout', {}, { withCredentials: true })
        .then(() => {
            localStorage.removeItem('user');
        })
        .catch(error => {
            console.error('Logout failed:', error);
        });
};

const checkAuth = () => {
    return axios.get(API_URL + 'check', { withCredentials: true })
        .then(response => response.status === 200)
        .catch(() => false);
};

export default {
    register,
    login,
    logout,
    checkAuth
};