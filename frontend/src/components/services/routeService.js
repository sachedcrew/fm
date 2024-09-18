import axios from 'axios';

const API_URL = 'https://localhost:7049/api/routes/';

const getRoutes = () => {
    return axios.get(API_URL);
};

const getRoute = (id) => {
    return axios.get(API_URL + id);
};

const createRoute = (route) => {
    return axios.post(API_URL, route);
};

const updateRoute = (id, route) => {
    return axios.put(API_URL + id, route);
};

const deleteRoute = (id) => {
    return axios.delete(API_URL + id);
};

export default {
    getRoutes,
    getRoute,
    createRoute,
    updateRoute,
    deleteRoute
};
