import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import LoginForm from '../components/LoginForm';

test('renders login form', () => {
    render(<LoginForm />);
    expect(screen.getByLabelText(/Email/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Password/i)).toBeInTheDocument();
});

test('submits login form', () => {
    const handleSubmit = jest.fn();
    render(<LoginForm onSubmit={handleSubmit} />);
    fireEvent.change(screen.getByLabelText(/Email/i), { target: { value: 'test@example.com' } });
    fireEvent.change(screen.getByLabelText(/Password/i), { target: { value: 'password' } });
    fireEvent.click(screen.getByText(/Login/i));
    expect(handleSubmit).toHaveBeenCalled();
});
