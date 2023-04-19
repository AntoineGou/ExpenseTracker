import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import App from './App';

test('App starts up and renders without crashing', () => {
    const { getByText } = render(<App />);
    expect(getByText(/Expense Tracker/i)).toBeInTheDocument();
});