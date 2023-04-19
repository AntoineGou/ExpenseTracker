import React, { useState, useEffect } from 'react';
import AddTransaction from '../components/AddTransaction';
import TransactionList from '../components/TransactionList';
import { Transaction as TransactionType } from '../types/Transaction';
import Typography from '@mui/material/Typography';
import { fetchTransactions } from '../api/api';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Home: React.FC = () => {
    const [transactions, setTransactions] = useState<TransactionType[]>([]);

    const refreshTransactions = async () => {
        try {
            const fetchedTransactions = await fetchTransactions();
            setTransactions(fetchedTransactions);
        } catch (error) {
            console.error('Error fetching transactions:', error);
        }
    };

    const handleDeleteTransaction = (id: number) => {
        setTransactions(transactions.filter((transaction) => transaction.id !== id));
    };

    useEffect(() => {
        refreshTransactions();
    }, []);

    return (
        <div>
            <ToastContainer
                position="bottom-center"
                autoClose={5000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme="colored"
            />
            <Typography variant="h4" component="h1" gutterBottom>
                Expense Tracker
            </Typography>
            <AddTransaction refreshTransactions={refreshTransactions} />
            <TransactionList
                transactions={transactions}
                refreshTransactions={refreshTransactions}
                onDeleteTransaction={handleDeleteTransaction}
            />

        </div>
    );
};

export default Home;