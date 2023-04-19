import { Transaction as TransactionType } from '../types/Transaction';


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
        throw new Error(`Error updating transaction: ${response.statusText}`);
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
        console.error('Error fetching transactions:', error);
        throw error;
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
        throw new Error('Error adding transaction');
    }
};

export const deleteTransaction = async (id: number): Promise<void> => {
    const response = await fetch(`/api/Expenses/${id}`, {
        method: 'DELETE',
    });

    if (!response.ok) {
        throw new Error(`Error deleting transaction: ${response.statusText}`);
    }
};

