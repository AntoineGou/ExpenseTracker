import React, { useState } from 'react';
import { Transaction as TransactionType } from '../types/Transaction';
import { deleteTransaction } from '../api/api';
import TableCell from '@mui/material/TableCell';
import TableRow from '@mui/material/TableRow';
import TextField from '@mui/material/TextField';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';
import SaveIcon from '@mui/icons-material/Save';
import ClearIcon from '@mui/icons-material/Clear';
import DeleteIcon from '@mui/icons-material/Delete';

interface EditableTransactionProps {
    transaction: TransactionType;
    onUpdateTransaction: (id: number, updatedTransaction: TransactionType) => void;
    onDeleteTransaction: (id: number) => void;
}

const EditableTransaction: React.FC<EditableTransactionProps> = ({
    transaction,
    onUpdateTransaction,
    onDeleteTransaction,
}) => {
    const [isEditMode, setIsEditMode] = useState(false);
    const [editedTransaction, setEditedTransaction] = useState(transaction);

    const handleChange = (field: keyof TransactionType) => (
        event: React.ChangeEvent<HTMLInputElement>
    ) => {
        setEditedTransaction({
            ...editedTransaction,
            [field]: event.target.value,
        });
    };

    const handleEdit = () => {
        setIsEditMode(true);
    };

    const handleSave = () => {
        onUpdateTransaction(transaction.id, editedTransaction);
        setIsEditMode(false);
    };

    const handleCancel = () => {
        setEditedTransaction(transaction);
        setIsEditMode(false);
    };

    const handleDelete = async () => {
        try {
            await deleteTransaction(transaction.id);
            onDeleteTransaction(transaction.id);
        } catch (error) {
            console.error('Error deleting transaction:', error);
        }
    };

    return (
        <TableRow>
            {isEditMode ? (
                <>
                    <TableCell>
                        <TextField
                            type="date"
                            value={editedTransaction.date}
                            onChange={handleChange('date')}
                        />
                    </TableCell>
                    <TableCell>
                        <TextField
                            type="number"
                            value={editedTransaction.amount}
                            onChange={handleChange('amount')}
                        />
                    </TableCell>
                    <TableCell>
                        <TextField value={editedTransaction.recipient} onChange={handleChange('recipient')} />
                    </TableCell>
                    <TableCell>
                        <TextField value={editedTransaction.currency} onChange={handleChange('currency')} />
                    </TableCell>
                    <TableCell>
                        <TextField value={editedTransaction.type} onChange={handleChange('type')} />
                    </TableCell>
                    <TableCell>
                        <IconButton onClick={handleSave}>
                            <SaveIcon />
                        </IconButton>
                        <IconButton onClick={handleCancel}>
                            <ClearIcon />
                        </IconButton>
                        <IconButton onClick={handleDelete}>
                            <DeleteIcon />
                        </IconButton>
                    </TableCell>
                </>
            ) : (
                <>
                    <TableCell>{transaction.date}</TableCell>
                    <TableCell>{transaction.amount}</TableCell>
                    <TableCell>{transaction.recipient}</TableCell>
                    <TableCell>{transaction.currency}</TableCell>
                    <TableCell>{transaction.type}</TableCell>
                    <TableCell>
                        <IconButton onClick={handleEdit}>
                            <EditIcon />
                        </IconButton>
                        <IconButton onClick={handleDelete}>
                            <DeleteIcon />
                        </IconButton>
                    </TableCell>
                </>
            )}
        </TableRow>
    );
};

export default EditableTransaction;
