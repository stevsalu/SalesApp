import React from "react";
import type { CartItem } from "../App";

interface CartSummaryProps {
    cart: CartItem[];
    resetCart: () => void;
}

const CartSummary: React.FC<CartSummaryProps> = ({ cart, resetCart }) => {
    const total = cart.reduce((sum, item) => sum + item.price * item.quantity, 0);

    return (
        <div style={{ marginTop: "20px" }}>
            <h2>Total: €{total.toFixed(2)}</h2>
            <button onClick={resetCart}>Reset</button>
        </div>
    );
};

export default CartSummary;
