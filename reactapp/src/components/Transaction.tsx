import React from 'react';
import { Transaction as TransactionType } from '../types/Transaction';
import TableRow from '@mui/material/TableRow';
import TableCell from '@mui/material/TableCell';

interface TransactionProps {
    transaction: TransactionType;
}

const Transaction: React.FC<TransactionProps> = ({ transaction }) => {
    return (
        <TableRow>
            <TableCell>{transaction.date}</TableCell>
            <TableCell>{transaction.amount}</TableCell>
            <TableCell>{transaction.recipient}</TableCell>
            <TableCell>{transaction.currency}</TableCell>
            <TableCell>{transaction.type}</TableCell>
        </TableRow>
    );
};

export default Transaction;
