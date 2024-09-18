import React from 'react';
import { render } from '@testing-library/react';
import RouteCreate from '../RouteCreate';
import '@testing-library/jest-dom/extend-expect';

test('matches the snapshot', () => {
    const mockOnCreate = jest.fn();

    const { asFragment } = render(<RouteCreate onCreate={mockOnCreate} />);

    expect(asFragment()).toMatchSnapshot();
});
