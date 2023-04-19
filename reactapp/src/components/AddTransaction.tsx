import React, { useState } from 'react';
import { addTransaction } from '../api/api';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';

interface AddTransactionProps {
    refreshTransactions: () => void;
}


const AddTransaction: React.FC<AddTransactionProps> = ({ refreshTransactions }) => {
    const [date, setDate] = useState('');
    const [amount, setAmount] = useState(0);
    const [recipient, setRecipient] = useState('');
    const [currency, setCurrency] = useState('');
    const [type, setType] = useState<'food' | 'drinks' | 'other' | string>('food');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const newTransaction = {
            id: 0,
            date,
            amount,
            recipient,
            currency,
            type,
        };

        try {
            await addTransaction(newTransaction); // Replace submitTransaction with addTransaction
            setDate('');
            setAmount(0);
            setRecipient('');
            setCurrency('');
            setType('');
            refreshTransactions(); // Refresh the transactions after adding a new one
        } catch (error) {
            console.error('Error adding transaction:', error);
        }
    };


    return (
        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 4 }}>
            <h2>Add Transaction</h2>
            <TextField
                label="Date"
                type="date"
                value={date}
                onChange={(e) => setDate(e.target.value)}
                required
                sx={{ mr: 2 }}
            />
            <TextField
                label="Amount"
                type="number"
                value={amount}
                onChange={(e) => setAmount(+e.target.value)}
                required
                sx={{ mr: 2 }}
            />
            <TextField
                label="Recipient"
                type="text"
                value={recipient}
                onChange={(e) => setRecipient(e.target.value)}
                required
                sx={{ mr: 2 }}
            />
            <FormControl sx={{ minWidth: 120, mr: 2 }}>
                <InputLabel id="currency-label">Currency</InputLabel>
                <Select
                    labelId="currency-label"
                    value={currency}
                    onChange={(e) => setCurrency(e.target.value)}
                    required
                >
                    <MenuItem value="">
                        <em>Select Currency</em>
                    </MenuItem>
                    <MenuItem value="USD">USD</MenuItem>
                    <MenuItem value="EUR">EUR</MenuItem>
                </Select>
            </FormControl>
            <FormControl sx={{ minWidth: 120, mr: 2 }}>
                <InputLabel id="type-label">Type</InputLabel>
                <Select
                    labelId="type-label"
                    value={type}
                    onChange={(e) => setType(e.target.value)}
                    required
                >
                    <MenuItem value="">
                        <em>Select Type</em>
                    </MenuItem>
                    <MenuItem value="food">Food</MenuItem>
                    <MenuItem value="drinks">Drinks</MenuItem>
                    <MenuItem value="other">Other</MenuItem>
                </Select>
            </FormControl>
            <Button type="submit" variant="contained">
                Add
            </Button>
        </Box>
    );
};

export default AddTransaction;
