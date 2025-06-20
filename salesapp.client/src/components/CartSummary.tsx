import React from "react";
import type { CartItem } from "../App";

interface CartSummaryProps {
    cart: CartItem[];
    resetCart: () => void;
}

const CartSummary: React.FC<CartSummaryProps> = ({ cart, resetCart }) => {
    const total = cart.reduce((sum, item) => sum + item.price * item.quantity, 0);

    return (
        <div className="card p-3 mb-3" style={{ minWidth: "300px" }}>
            <h2>Cart Summary</h2>

            {cart.length === 0 ? (
                <p>Your cart is empty.</p>
            ) : (
                <ul className="list-group mb-3">
                        {cart.map((item) => (
                        <li key={item.productId} className="list-group-item d-flex justify-content-between align-items-center">
                            {item.name} (x{item.quantity})
                            <span>{"\u20AC"}{(item.price * item.quantity).toFixed(2)}</span>
                        </li>
                    ))}
                </ul>
            )}

            <p className="fw-bold">Total: {"\u20AC"}{total.toFixed(2)}</p>

            <button onClick={resetCart} className="btn btn-warning">
                Reset Cart
            </button>
        </div>
    );
};

export default CartSummary;
