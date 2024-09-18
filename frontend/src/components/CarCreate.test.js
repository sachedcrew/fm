import React from 'react';
import { render, screen } from '@testing-library/react';
import CarCreate from '../CarCreate';
import '@testing-library/jest-dom/extend-expect';

jest.mock('../services/carService', () => ({
    createCar: jest.fn().mockResolvedValue({ data: {} }),
}));

jest.mock('../services/carApiService', () => ({
    getCarMakes: jest.fn().mockResolvedValue({ data: [{ id: 1, name: 'Toyota' }, { id: 2, name: 'Honda' }] }),
    getCarModels: jest.fn().mockResolvedValue({ data: [{ id: 1, name: 'Camry' }, { id: 2, name: 'Accord' }] }),
}));

test('CarCreate component matches snapshot', () => {
    const mockOnCreate = jest.fn();

    const { asFragment } = render(
        <CarCreate onCreate={mockOnCreate} />
    );

    expect(asFragment()).toMatchSnapshot();
});
