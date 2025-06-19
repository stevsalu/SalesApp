import React from "react";
import type { Product } from "../App";

interface ProductCardProps {
    product: Product;
    addToCart: (product: Product) => void;
}

const ProductCard: React.FC<ProductCardProps> = ({ product, addToCart }) => {
    const isOutOfStock = product.quantity === 0;

    const handleClick = () => {
        if (!isOutOfStock) {
            addToCart(product);
        }
    };

    return (
        <div
            onClick={handleClick}
            style={{
                width: "150px",
                margin: "10px",
                opacity: isOutOfStock ? 0.5 : 1,
                cursor: isOutOfStock ? "not-allowed" : "pointer",
                border: "1px solid gray",
                padding: "10px"
            }}
        >
            <img src={`/images/${product.name}.jpg`} alt={product.name} width="100%" />
            <h3>{product.name}</h3>
            <p>€{product.price.toFixed(2)}</p>
            <p>In stock: {product.quantity}</p>
        </div>
    );
};

export default ProductCard;
