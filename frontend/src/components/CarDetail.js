import React, { useEffect, useState } from 'react';
import carService from './services/carService';

const CarDetail = ({ carId, onBack }) => {
    const [car, setCar] = useState(null);

    useEffect(() => {
        const fetchCarDetails = async () => {
            try {
                const response = await carService.getCar(carId);
                setCar(response.data);
            } catch (error) {
                console.error('Error fetching car details:', error);
            }
        };

        fetchCarDetails();
    }, [carId]);

    if (!car) return <div>Loading...</div>;

    return (
        <div>
            <h3>Car Details</h3>
            <p><strong>Make:</strong> {car.make}</p>
            <p><strong>Model:</strong> {car.model}</p>
            <p><strong>Year:</strong> {car.year}</p>
            <p><strong>Color:</strong> {car.color}</p>
            <p><strong>Price:</strong> ${car.price}</p>
            <button onClick={onBack}>Back to List</button>
        </div>
    );
};

export default CarDetail;
