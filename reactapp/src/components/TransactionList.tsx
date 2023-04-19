import React, { useEffect, useState } from 'react';
import { Transaction as TransactionType } from '../types/Transaction';
import EditableTransaction from './EditableTransaction';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Typography from '@mui/material/Typography';
import { updateTransaction } from "../api/api"

interface TransactionListProps {
    transactions: TransactionType[];
    refreshTransactions: () => void;
    onDeleteTransaction: (id: number) => void;
}

const TransactionList: React.FC<TransactionListProps> = ({
    transactions,
    refreshTransactions,
    onDeleteTransaction,
}) => {

    useEffect(() => {
        refreshTransactions();
    }, [refreshTransactions]);

    const handleUpdateTransaction = async (id: number, updatedTransaction: TransactionType) => {
        try {
            await updateTransaction(id, updatedTransaction);
            refreshTransactions();
        } catch (error) {
            console.error('Error updating transaction:', error);
        }
    };

    return (
        <>
<Typography variant="h5" component="h2" gutterBottom>
    Transactions
</Typography>
<TableContainer component={Paper}>
    <Table>
        <TableHead>
            <TableRow>
                <TableCell>Date</TableCell>
                <TableCell>Amount</TableCell>
                <TableCell>Recipient</TableCell>
                <TableCell>Currency</TableCell>
                <TableCell>Type</TableCell>
                <TableCell>Edit</TableCell> {/* Add TableCell for Edit */}
            </TableRow>
        </TableHead>
        <TableBody>
            {transactions.map((transaction) => (
                            <EditableTransaction
                                key={transaction.id}
                                transaction={transaction}
                                onUpdateTransaction={handleUpdateTransaction}
                                onDeleteTransaction={onDeleteTransaction}
                            />
                        ))}
        </TableBody>
    </Table>
</TableContainer>
</>
    );
};

export default TransactionList;
