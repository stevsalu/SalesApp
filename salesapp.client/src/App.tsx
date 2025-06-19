import React, { useEffect, useState } from "react";
import ProductGrid from "./components/ProductGrid";
import CartSummary from "./components/CartSummary";
import CheckoutForm from "./components/CheckoutForm";
import axios from "axios";

export interface Product {
    id: string;
    name: string;
    price: number;
    quantity: number;
    categoryId: number;
    categoryName: string;
}

export interface CartItem {
    productId: string;
    name: string;
    price: number;
    quantity: number;
}

function App() {
    const [products, setProducts] = useState<Product[]>([]);
    const [cart, setCart] = useState<CartItem[]>([]);

    useEffect(() => {
        loadProducts();
    }, []);

    const loadProducts = async () => {
        const response = await fetch("/api/products");
        console.log(response.json());
        //setProducts(response.data);
    };

    const addToCart = (product: Product) => {
        setCart(prevCart => {
            const existing = prevCart.find(item => item.productId === product.id);
            if (existing) {
                return prevCart.map(item =>
                    item.productId === product.id
                        ? { ...item, quantity: item.quantity + 1 }
                        : item
                );
            } else {
                return [...prevCart, { productId: product.id, name: product.name, price: product.price, quantity: 1 }];
            }
        });
    };

    const resetCart = () => {
        setCart([]);
    };

    return (
        <div>
            <h1>Point of Sale</h1>
            <ProductGrid products={products} addToCart={addToCart} />
            <CartSummary cart={cart} resetCart={resetCart} />
            <CheckoutForm cart={cart} onCheckoutSuccess={() => {
                resetCart();
                loadProducts();
            }} />
        </div>
    );
}

export default App;
