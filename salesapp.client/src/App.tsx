import { useEffect, useState } from "react";
import ProductGrid from "./components/ProductGrid";
import CartSummary from "./components/CartSummary";
import CheckoutForm from "./components/CheckoutForm";
import axios from "axios";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import AdminPanel from "./components/AdminPanel";
import useProductHub from "./hooks/ProductHub";

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
    const [showAdmin, setShowAdmin] = useState(false);

    useEffect(() => {
        loadProducts();
    }, []);

    const loadProducts = async () => {
        try {
            const response = await axios.get("/api/product");
            setProducts(response.data);
        } catch(error: any) {
            toast.error(error.response?.data?.errorMessage || "Loading products failed");
        }
    };

    const addToCart = async (product: Product) => {
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

        setProducts((prev) => decreaseProductQuantity(prev, product.id, 1));

        await axios.post("/api/cart/reserve", {
            productId: product.id,
            quantity: 1
        });
    };

    const resetCart = () => {
        setCart([]);
    };

    const toggleAdmin = () => {
        if (showAdmin) {
            loadProducts();
        }
        setShowAdmin(!showAdmin);
    };

    const decreaseProductQuantity = (
        products: Product[],
        productId: string,
        quantityToDecrease: number
    ): Product[] => {
        return products.map((p) =>
            p.id === productId ? { ...p, quantity: p.quantity - quantityToDecrease } : p
        );
    };

    const handleProductUpdated = (updatedProduct: Product) => {
        setProducts((prev) =>
            prev.map((p) => (p.id === updatedProduct.id ? updatedProduct : p))
        );
    };

    const handleProductReserved = (id: string, quantity: number) => {
        setProducts((prev) => decreaseProductQuantity(prev, id, quantity));

    }

    useProductHub({
        onProductUpdated: handleProductUpdated,
        onProductReserved: handleProductReserved
    });

    return (
        <div className="container mt-4">
            <button className="btn btn-secondary mb-3" onClick={toggleAdmin}>
                {showAdmin ? "Back to Sales" : "Admin Panel"}
            </button>

            {showAdmin ? (
                <AdminPanel />
            ) : (
                <>
                    <ProductGrid products={products} addToCart={addToCart} />
                    <div className="d-flex flex-wrap gap-3 mt-4">
                            <CartSummary cart={cart} resetCart={resetCart} />
                            <CheckoutForm cart={cart} onCheckoutSuccess={() => {
                                resetCart();
                                loadProducts();
                            }} />
                    </div>
                </>
            )}
        </div>

    );
}

export default App;
