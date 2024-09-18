import React from 'react';

const CarList = ({ cars, onDelete, onEdit }) => {
    return (
        <div className="container mt-5">
            <h2 className="mb-4">Cars</h2>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Make</th>
                        <th>Model</th>
                        <th>Year</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {cars.map(car => (
                        <tr key={car.id}>
                            <td>{car.id}</td>
                            <td>{car.make}</td>
                            <td>{car.model}</td>
                            <td>{car.year}</td>
                            <td>
                                <button 
                                    className="btn btn-warning btn-sm me-2" 
                                    onClick={() => onEdit(car)}
                                >
                                    Edit
                                </button>
                                <button 
                                    className="btn btn-danger btn-sm" 
                                    onClick={() => onDelete(car.id)}
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

export default CarList;
