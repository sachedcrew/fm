import axios from 'axios';

const API_URL = 'https://localhost:7049/api/cars/';

const getCars = () => {
    return axios.get(API_URL);
};

const getCar = (id) => {
    return axios.get(API_URL + id);
};

const createCar = (car) => {
    return axios.post(API_URL, car);
};

const updateCar = (id, car) => {
    return axios.put(API_URL + id, car);
};

const deleteCar = (id) => {
    return axios.delete(API_URL + id);
};

export default {
    getCars,
    getCar,
    createCar,
    updateCar,
    deleteCar
};
