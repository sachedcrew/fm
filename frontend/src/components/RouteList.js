import React from 'react';

const RouteList = ({ routes, onDelete, onEdit }) => {
    return (
        <div className="container mt-5">
            <h2 className="mb-4">Routes</h2>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Car ID</th>
                        <th>Start Location</th>
                        <th>End Location</th>
                        <th>Start Time</th>
                        <th>End Time</th>
                        <th>Distance</th>
                        <th>Fuel Used</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {routes.map(route => (
                        <tr key={route.id}>
                            <td>{route.id}</td>
                            <td>{route.carId}</td>
                            <td>{route.startLocation}</td>
                            <td>{route.endLocation}</td>
                            <td>{new Date(route.startTime).toLocaleString()}</td>
                            <td>{new Date(route.endTime).toLocaleString()}</td>
                            <td>{route.distance}</td>
                            <td>{route.fuelUsed}</td>
                            <td>
                                <button 
                                    className="btn btn-warning btn-sm me-2" 
                                    onClick={() => onEdit(route)}
                                >
                                    Edit
                                </button>
                                <button 
                                    className="btn btn-danger btn-sm" 
                                    onClick={() => onDelete(route.id)}
                                >
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default RouteList;
