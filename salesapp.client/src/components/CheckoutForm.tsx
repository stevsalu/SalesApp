import React, { useState } from "react";
import axios from "axios";
import type { CartItem } from "../App";

interface CheckoutFormProps {
    cart: CartItem[];
    onCheckoutSuccess: () => void;
}

const CheckoutForm: React.FC<CheckoutFormProps> = ({ cart, onCheckoutSuccess }) => {
    const [cashPaid, setCashPaid] = useState<string>("");
    const [message, setMessage] = useState<string>("");

    const handleCheckout = async () => {
        const payload = {
            cashPaid: parseFloat(cashPaid),
            items: cart.map(item => ({
                productId: item.productId,
                quantity: item.quantity
            }))
        };

        try {
            const response = await axios.post("/api/checkout", payload);
            const result = response.data;
            setMessage(`Change to return: €${result.changeReturned.toFixed(2)}`);
            onCheckoutSuccess();
        } catch (error: any) {
            if (error.response?.data?.errorMessage) {
                setMessage(`Error: ${error.response.data.errorMessage}`);
            } else {
                setMessage("Error during checkout.");
            }
        }
    };

    return (
        <div style={{ marginTop: "20px" }}>
            <h2>Checkout</h2>
            <input
                type="number"
                placeholder="Cash paid"
                value={cashPaid}
                onChange={(e) => setCashPaid(e.target.value)}
            />
            <button onClick={handleCheckout}>Checkout</button>
            {message && <p>{message}</p>}
        </div>
    );
};

export default CheckoutForm;
