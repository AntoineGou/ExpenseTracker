import { Transaction as TransactionType } from '../types/Transaction';
import { toast } from 'react-toastify';

const handleApiError = (error: unknown, message: string, responseStatusText?: string): void => {
    toast(message);
    console.error(`${message}:`, error);
    throw new Error(responseStatusText || 'An error occurred');
};

export async function updateTransaction(
    id: number,
    transaction: TransactionType
): Promise<TransactionType> {
    const response = await fetch(`/api/Expenses/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(transaction),
    });

    if (!response.ok) {
        handleApiError(new Error('Error updating transaction'), 'Error updating transaction', response.statusText);
    }

    const updatedTransaction: TransactionType = await response.json();
    return updatedTransaction;
}

export const fetchTransactions = async (): Promise<TransactionType[]> => {
    try {
        const response = await fetch('/api/Expenses');
        const data = await response.json();
        return data;
    } catch (error) {
        handleApiError(error, 'Error fetching transactions');
        return Promise.reject(error);
    }
};

export const addTransaction = async (transaction: TransactionType): Promise<void> => {
    const response = await fetch('/api/Expenses', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(transaction),
    });

    if (!response.ok) {
        handleApiError(new Error('Error adding transaction'), 'Error adding transaction', response.statusText);
    }
};

export const deleteTransaction = async (id: number): Promise<void> => {
    const response = await fetch(`/api/Expenses/${id}`, {
        method: 'DELETE',
    });

    if (!response.ok) {
        handleApiError(new Error('Error deleting transaction'), 'Error deleting transaction', response.statusText);
    }
};
