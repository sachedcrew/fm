import React, { useState, useEffect } from 'react';
import carService from './services/carService';

const CarEdit = ({ car, onUpdate, onBackToCreate }) => {
    const [updatedCar, setUpdatedCar] = useState(car);

    useEffect(() => {
        setUpdatedCar(car);
    }, [car]);
    

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUpdatedCar({ ...updatedCar, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await carService.updateCar(updatedCar.id, updatedCar);
            onUpdate(response.data); // Aktualizacja listy samochodów po edytowaniu
        } catch (error) {
            console.error('Error updating car:', error);
        }
    };

    return (
        <div className="container mt-5">
            <div className="card">
                <div className="card-body">
                    <h3 className="card-title mb-4">Edit Car</h3>
                    <form onSubmit={handleSubmit}>
                        <div className="mb-3">
                            <label htmlFor="make" className="form-label">Make:</label>
                            <input
                                type="text"
                                id="make"
                                name="make"
                                className="form-control"
                                value={updatedCar.make}
                                onChange={handleChange}
                                required
                            />
                        </div>
                        <div className="mb-3">
                            <label htmlFor="model" className="form-label">Model:</label>
                            <input
                                type="text"
                                id="model"
                                name="model"
                                className="form-control"
                                value={updatedCar.model}
                                onChange={handleChange}
                                required
                            />
                        </div>
                        <div className="mb-3">
                            <label htmlFor="year" className="form-label">Year:</label>
                            <input
                                type="text"
                                id="year"
                                name="year"
                                className="form-control"
                                value={updatedCar.year}
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
                                value={updatedCar.vin}
                                onChange={handleChange}
                                required
                            />
                        </div>
                        <button type="submit" className="btn btn-primary">Update Car</button>
                        <button
                            type="button"
                            className="btn btn-secondary ms-3"
                            onClick={onBackToCreate}
                        >
                            Back to Create Car
                        </button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default CarEdit;
