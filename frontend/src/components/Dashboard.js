import React, { useEffect, useState } from 'react';
import authService from './services/authService';
import carService from './services/carService';
import routeService from './services/routeService';
import CarList from './CarList';
import RouteList from './RouteList';
import CarCreate from './CarCreate';
import CarEdit from './CarEdit';
import RouteCreate from './RouteCreate';
import RouteEdit from './RouteEdit';

const Dashboard = () => {
    const [cars, setCars] = useState([]);
    const [routes, setRoutes] = useState([]);
    const [editingCar, setEditingCar] = useState(null);
    const [editingRoute, setEditingRoute] = useState(null);
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        // Check if the user is authenticated
        const checkAuth = async () => {
            try {
                const isAuth = await authService.checkAuth();
                setIsAuthenticated(isAuth);
            } catch (error) {
                console.error('Error checking authentication:', error);
                setIsAuthenticated(false);
            }
        };

        checkAuth();

        const fetchCars = async () => {
            const response = await carService.getCars();
            setCars(response.data);
        };

        const fetchRoutes = async () => {
            const response = await routeService.getRoutes();
            setRoutes(response.data);
        };

        fetchCars();
        fetchRoutes();
    }, []);

    const handleCarCreate = (newCar) => {
        setCars([...cars, newCar]);
    };

    const handleCarDelete = async (id) => {
        try {
            await carService.deleteCar(id);
            setCars(cars.filter(car => car.id !== id));
        } catch (error) {
            console.error('Error deleting car:', error);
        }
    };

    const handleCarEdit = (car) => {
        setEditingCar(car);
    };

    const handleCarUpdate = (updatedCar) => {
        setCars(cars.map(car => (car.id === updatedCar.id ? updatedCar : car)));
        setEditingCar(null); // End editing
    };

    const handleRouteCreate = (newRoute) => {
        setRoutes([...routes, newRoute]);
    };

    const handleRouteDelete = async (id) => {
        try {
            await routeService.deleteRoute(id);
            setRoutes(routes.filter(route => route.id !== id));
        } catch (error) {
            console.error('Error deleting route:', error);
        }
    };

    const handleRouteEdit = (route) => {
        setEditingRoute(route);
    };

    const handleRouteUpdate = (updatedRoute) => {
        setRoutes(routes.map(route => (route.id === updatedRoute.id ? updatedRoute : route)));
        setEditingRoute(null); // End editing
    };

    const handleLogout = async () => {
        try {
            await authService.logout();
            setIsAuthenticated(false);
            window.location.href = '/login'; // Redirect to login page
        } catch (error) {
            console.error('Logout failed:', error);
        }
    };

    return (
        <div>
            <div className="container mt-3">
                <div className="row">
                    <div className="col">
                        <div className="card">
                            <div className="card-body">
                                <CarList cars={cars} onDelete={handleCarDelete} onEdit={handleCarEdit} />
                            </div>
                        </div>
                    </div>
                    <div className="col">
                        <div className="card">
                            <div className="card-body">
                                {editingCar ? (
                                    <CarEdit
                                        car={editingCar}
                                        onUpdate={handleCarUpdate}
                                        onBackToCreate={() => setEditingCar(null)}
                                    />
                                ) : (
                                    <CarCreate onCreate={handleCarCreate} />
                                )}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="container">
                <div className="row">
                    <div className="col">
                        <div className="card">
                            <div className="card-body">
                                <h2>Routes</h2>
                                <RouteList routes={routes} onDelete={handleRouteDelete} onEdit={handleRouteEdit} />
                            </div>
                        </div>
                    </div>
                    <div className="col">
                        <div className="card">
                            <div className="card-body">
                                {editingRoute ? (
                                    <RouteEdit route={editingRoute} onUpdate={handleRouteUpdate} onBackToCreate={() => setEditingRoute(null)} />
                                ) : (
                                    <RouteCreate onCreate={handleRouteCreate} />
                                )}
                            </div>
                        </div>
                    </div>
                </div>
            </div>

           
        </div>
    );
};

export default Dashboard;
