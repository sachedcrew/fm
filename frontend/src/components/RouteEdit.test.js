import React from 'react';
import { render, screen } from '@testing-library/react';
import RouteCreate from '../RouteCreate';
import '@testing-library/jest-dom/extend-expect';

jest.mock('../services/routeService', () => ({
    createRoute: jest.fn().mockResolvedValue({ data: {} }),
}));

jest.mock('../services/carService', () => ({
    getCars: jest.fn().mockResolvedValue({
        data: [
            { id: 1, make: 'Toyota', model: 'Camry', year: 2022 },
            { id: 2, make: 'Honda', model: 'Accord', year: 2023 },
        ],
    }),
}));

test('RouteCreate component matches snapshot', () => {
    const mockOnCreate = jest.fn();

    const { asFragment } = render(<RouteCreate onCreate={mockOnCreate} />);

    expect(asFragment()).toMatchSnapshot();
});
