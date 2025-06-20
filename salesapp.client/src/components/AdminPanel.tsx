import React, { useEffect, useState } from "react";
import axios from "axios";
import { toast } from "react-toastify";
import type { Product } from "../App";

const AdminPanel: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
        loadProducts();
    }, []);

    const loadProducts = async () => {
        try {
            const response = await axios.get("/api/product");
            setProducts(response.data);
        } catch (error) {
            toast.error("Failed to load products");
        }
    };

    const handleSave = async (product: Product) => {
        try {
            await axios.put(`/api/product/${product.id}`, {
                name: product.name,
                price: product.price,
                quantity: product.quantity,
                categoryId: product.categoryId
            });
            toast.success("Product updated!");
        } catch (error) {
            toast.error("Failed to update product");
        }
    };

    const handleQuantityChange = (id: string, quantity: number) => {
        setProducts(products.map(p =>
            p.id === id ? { ...p, quantity } : p
        ));
    };

    return (
        <div className="container mt-4">
            <h2>Admin Panel</h2>

            <table className="table table-striped table-bordered">
                <thead className="table-light">
                    <tr>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Quantity</th>
                        <th>Category</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product => (
                        <tr key={product.id}>
                            <td>{product.name}</td>
                            <td>{"\u20AC"}{product.price.toFixed(2)}</td>
                            <td>
                                <input
                                    type="number"
                                    value={product.quantity}
                                    onChange={(e) =>
                                        handleQuantityChange(product.id, parseInt(e.target.value))
                                    }
                                    className="form-control"
                                    style={{ maxWidth: "100px" }}
                                />
                            </td>
                            <td>{product.categoryName}</td>
                            <td>
                                <button className="btn btn-primary" onClick={() => handleSave(product)}>Save</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default AdminPanel;
