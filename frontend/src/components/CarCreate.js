import React, { useState, useEffect } from 'react';
import carApiService from './services/carApiService'; // Importujemy nasz nowy serwis
import carService from './services/carService';

const CarCreate = ({ onCreate }) => {
    const [car, setCar] = useState({ make: '', model: '', year: '', vin: '' });
    const [makes, setMakes] = useState([]);
    const [models, setModels] = useState([]);
    const [selectedMake, setSelectedMake] = useState('');

    useEffect(() => {
        // Pobieranie marek samochodów po załadowaniu komponentu
        const fetchMakes = async () => {
            try {
                const response = await carApiService.getCarMakes();
                const carMakes = response.data; 
                setMakes(carMakes);
            } catch (error) {
                console.error("Error fetching car makes:", error);
            }
        };
        const fetchModels = async (e) => {
            try {
                const response = await carApiService.getCarModels();
                const carModels = response.data; 
                setModels(carModels);
            } catch (error) {
                console.error("Error fetching car models:", error);
            }
    };

        fetchMakes();
        fetchModels();
    }, []);

    

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCar({ ...car, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await carService.createCar(car);
            onCreate(response.data); // Aktualizacja listy samochodów po dodaniu
        } catch (error) {
            console.error('Error creating car:', error);
        }
    };

    return (
        <div className="container mt-5">
            <div className="card">
                <div className="card-body">
                    <h3 className="card-title mb-4">Add New Car</h3>
                    <form onSubmit={handleSubmit}>
                        <div className="mb-3">
                            <label htmlFor="make" className="form-label">Make:</label>
                            <select
                                id="make"
                                name="make"
                                className="form-select"
                                value={car.make}
                                onChange={handleChange}
                                required
                            >
                                <option value="">Select Make</option>
                                {makes.map((make) => (
                                    <option key={make.id} value={make.name}>
                                        {make.name}
                                    </option>
                                ))}
                            </select>
                        </div>
                        <div className="mb-3">
                            <label htmlFor="model" className="form-label">Model:</label>
                            <select
                                id="model"
                                name="model"
                                className="form-select"
                                value={car.model}
                                onChange={handleChange}
                                required
                            >
                                <option value="">Select Model</option>
                                {models.map((model) => (
                                    <option key={model.id} value={model.name}>
                                        {model.name}
                                    </option>
                                ))}
                            </select>
                        </div>
                        <div className="mb-3">
                            <label htmlFor="year" className="form-label">Year:</label>
                            <input
                                type="text"
                                id="year"
                                name="year"
                                className="form-control"
                                value={car.year}
                                onChange={handleChange}
                                required
                            />
                        </div>
                        <div className="mb-3">
                            <label htmlFor="vin" className="form-label">VIN:</label>
                            <input
                                type="text"
                                id="vin"
                                name="vin"
                                className="form-control"
                                value={car.vin}
                                onChange={handleChange}
                                required
                            />
                        </div>
                        <button type="submit" className="btn btn-primary">Add Car</button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default CarCreate;
