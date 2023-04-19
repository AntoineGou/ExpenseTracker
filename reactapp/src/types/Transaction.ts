export interface Transaction {
    id: number;
    date: string;
    amount: number;
    recipient: string;
    currency: string;
    type: "food" | "drinks" | "other" | string;
}
