import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import routeService from './services/routeService';

const RouteDetail = () => {
    const { id } = useParams();
    const [route, setRoute] = useState(null);

    useEffect(() => {
        const fetchRoute = async () => {
            try {
                const response = await routeService.getRoute(id);
                setRoute(response.data);
            } catch (error) {
                console.error('Failed to fetch route:', error);
            }
        };

        fetchRoute();
    }, [id]);

    if (!route) return <div>Loading...</div>;

    return (
        <div>
            <h2>Route Details</h2>
            <p><strong>Start Location:</strong> {route.startLocation}</p>
            <p><strong>End Location:</strong> {route.endLocation}</p>
            <p><strong>Start Time:</strong> {new Date(route.startTime).toLocaleString()}</p>
            <p><strong>End Time:</strong> {new Date(route.endTime).toLocaleString()}</p>
            <p><strong>Distance:</strong> {route.distance} km</p>
            <p><strong>Fuel Used:</strong> {route.fuelUsed} liters</p>
            <Link to={`/routes/edit/${route.id}`}>Edit</Link>
            <Link to={`/routes/delete/${route.id}`}>Delete</Link>
        </div>
    );
};

export default RouteDetail;
