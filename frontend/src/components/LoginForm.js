import React, { useState } from 'react';
import authService from './services/authService';

const LoginForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [message, setMessage] = useState('');
    const [alertClass, setAlertClass] = useState(''); // for alert class (alert-success or alert-danger)

    const handleSubmit = async (event) => {
        event.preventDefault();
        setMessage('');
        setAlertClass('');

        try {
            await authService.login(email, password);
            window.location.href = '/dashboard';
        } catch (error) {
            setMessage(error.response.data.message);
            setAlertClass('alert-danger');
        }
    };

    return (
        <div className="container mt-5">
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <div className="card">
                        <div className="card-body">
                            <h3 className="card-title text-center mb-4">Login</h3>
                            <form onSubmit={handleSubmit}>
                                <div className="mb-3">
                                    <label htmlFor="email" className="form-label">Email address</label>
                                    <input 
                                        type="email" 
                                        className="form-control" 
                                        id="email" 
                                        placeholder="Enter email" 
                                        value={email} 
                                        onChange={(e) => setEmail(e.target.value)} 
                                        required 
                                    />
                                </div>

                                <div className="mb-3">
                                    <label htmlFor="password" className="form-label">Password</label>
                                    <input 
                                        type="password" 
                                        className="form-control" 
                                        id="password" 
                                        placeholder="Password" 
                                        value={password} 
                                        onChange={(e) => setPassword(e.target.value)} 
                                        required 
                                    />
                                </div>

                                <button type="submit" className="btn btn-primary w-100">Login</button>
                            </form>
                            {message && <div className={`alert ${alertClass} mt-3`} role="alert">{message}</div>}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default LoginForm;
