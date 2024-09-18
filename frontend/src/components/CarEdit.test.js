import React from 'react';
import { render, screen } from '@testing-library/react';
import CarEdit from '../CarEdit';
import '@testing-library/jest-dom/extend-expect';

jest.mock('../services/carService', () => ({
    updateCar: jest.fn().mockResolvedValue({ data: {} }),
}));

test('CarEdit component matches snapshot', () => {
    const mockOnUpdate = jest.fn();
    const mockOnBackToCreate = jest.fn();

    const car = {
        id: 1,
        make: 'Toyota',
        model: 'Camry',
        year: 2022,
        vin: '1234567890ABCDEFG'
    };

    const { asFragment } = render(
        <CarEdit car={car} onUpdate={mockOnUpdate} onBackToCreate={mockOnBackToCreate} />
    );

    expect(asFragment()).toMatchSnapshot();
});
