import axios from 'axios';

const API_URL = 'https://car-api2.p.rapidapi.com/api/'; 

const getCarMakes = async () => {
  try {
    const response = await axios.get(`${API_URL}makes`, {
      headers: {
        'X-RapidAPI-Host': 'car-api2.p.rapidapi.com',
        'X-RapidAPI-Key': '000a611dc3msh3c8976ec2ae982fp10cba1jsn037d86ef86bb'
      }
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching car makes:", error.message);
    throw error;
  }
};

const getCarModels = async (makeId) => {
  try {
    const response = await axios.get(`${API_URL}models`, {
      headers: {
        'X-RapidAPI-Host': 'car-api2.p.rapidapi.com',
        'X-RapidAPI-Key': '000a611dc3msh3c8976ec2ae982fp10cba1jsn037d86ef86bb'
      }
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching car models:", error.message);
    throw error;
  }
};

export default {
  getCarMakes,
  getCarModels
};
