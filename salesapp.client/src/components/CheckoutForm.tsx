import React, { useState } from "react";
import axios from "axios";
import type { CartItem } from "../App";
import { toast } from "react-toastify";

interface CheckoutFormProps {
    cart: CartItem[];
    onCheckoutSuccess: () => void;
}

const CheckoutForm: React.FC<CheckoutFormProps> = ({ cart, onCheckoutSuccess }) => {
    const [amountPaid, setAmountPaid] = useState<string>("");
    const [message, setMessage] = useState<string>("");

    const handleCheckout = async () => {
        const payload = {
            amountPaid: parseFloat(amountPaid),
            items: cart.map(item => ({
                productId: item.productId,
                quantity: item.quantity
            }))
        };

        try {
            const response = await axios.post("/api/checkout", payload);
            const result = response.data;
            toast.success(`Checkout successful! Change to return: ${"\u20AC"}${result.changeReturned.toFixed(2)}`);
            setMessage(`Change to return: ${ "\u20AC" }${ result.changeReturned.toFixed(2) }`)
            onCheckoutSuccess();
        } catch (error: any) {
            if (error.response?.data?.errorMessage) {
                toast.error(`Error: ${error.response.data.errorMessage}`);
            } else {
                toast.error("Error during checkout.");
            }
        }
    };

    return (
        <div className="card p-3">
            <h2>Checkout</h2>
            <input
                type="number"
                className="form-control mb-2"
                placeholder="Cash paid"
                value={amountPaid}
                onChange={(e) => setAmountPaid(e.target.value)}
            />
            <button onClick={handleCheckout} className="btn btn-success">
                Checkout
            </button>
        </div>
    );
};

export default CheckoutForm;
