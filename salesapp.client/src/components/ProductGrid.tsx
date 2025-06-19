import React from "react";
import ProductCard from "./ProductCard";
import type { Product } from "../App";

interface ProductGridProps {
    products: Product[];
    addToCart: (product: Product) => void;
}

const ProductGrid: React.FC<ProductGridProps> = ({ products, addToCart }) => {
    return (
        <div style={{ display: "flex", flexWrap: "wrap" }}>
            {products.map(product => (
                <ProductCard key={product.id} product={product} addToCart={addToCart} />
            ))}
        </div>
    );
};

export default ProductGrid;
