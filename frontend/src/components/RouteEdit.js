import React, { useState, useEffect } from 'react';
import routeService from './services/routeService';
import carService from './services/carService';

const RouteEdit = ({ route, onUpdate, onBackToCreate }) => {
    const [carId, setCarId] = useState(route.carId);
    const [startLocation, setStartLocation] = useState(route.startLocation);
    const [endLocation, setEndLocation] = useState(route.endLocation);
    const [startTime, setStartTime] = useState(route.startTime.slice(0, 16));
    const [endTime, setEndTime] = useState(route.endTime.slice(0, 16));
    const [distance, setDistance] = useState(route.distance);
    const [fuelUsed, setFuelUsed] = useState(route.fuelUsed);
    const [cars, setCars] = useState([]);

    useEffect(() => {
        const fetchCars = async () => {
            try {
                const response = await carService.getCars();
                setCars(response.data);
            } catch (error) {
                console.error('Failed to fetch cars:', error);
            }
        };

        fetchCars();
    }, []);

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (!carId) {
            console.error('CarId is required');
            return;
        }
        try {
            const updatedRoute = {
                id: route.id,
                carId: parseInt(carId, 10),
                startLocation,
                endLocation,
                startTime,
                endTime,
                distance: parseFloat(distance),
                fuelUsed: parseFloat(fuelUsed),
            };
            const response = await routeService.updateRoute(route.id, updatedRoute);
            onUpdate(response.data);
        } catch (error) {
            console.error('Failed to update route:', error);
        }
    };

    return (
        <div>
            <h2>Edit Route</h2>
            <form onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label htmlFor="carId" className="form-label">Car:</label>
                    <select
                        id="carId"
                        className="form-select"
                        value={carId}
                        onChange={(e) => setCarId(e.target.value)}
                        required
                    >
                        <option value="">Select a car</option>
                        {cars.map(car => (
                            <option key={car.id} value={car.id}>
                                {car.make} {car.model} ({car.year})
                            </option>
                        ))}
                    </select>
                </div>
                <div className="mb-3">
                    <label htmlFor="startLocation" className="form-label">Start Location:</label>
                    <input
                        type="text"
                        id="startLocation"
                        className="form-control"
                        value={startLocation}
                        onChange={(e) => setStartLocation(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="endLocation" className="form-label">End Location:</label>
                    <input
                        type="text"
                        id="endLocation"
                        className="form-control"
                        value={endLocation}
                        onChange={(e) => setEndLocation(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="startTime" className="form-label">Start Time:</label>
                    <input
                        type="datetime-local"
                        id="startTime"
                        className="form-control"
                        value={startTime}
                        onChange={(e) => setStartTime(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="endTime" className="form-label">End Time:</label>
                    <input
                        type="datetime-local"
                        id="endTime"
                        className="form-control"
                        value={endTime}
                        onChange={(e) => setEndTime(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="distance" className="form-label">Distance (km):</label>
                    <input
                        type="number"
                        id="distance"
                        className="form-control"
                        value={distance}
                        onChange={(e) => setDistance(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="fuelUsed" className="form-label">Fuel Used (liters):</label>
                    <input
                        type="number"
                        id="fuelUsed"
                        className="form-control"
                        value={fuelUsed}
                        onChange={(e) => setFuelUsed(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" className="btn btn-primary">Update Route</button>
            </form>
            <button className="btn btn-secondary mt-3" onClick={onBackToCreate}>Back to Create Route</button>
        </div>
    );
};

export default RouteEdit;
