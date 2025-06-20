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
            className={`card text-center ${isOutOfStock ? "opacity-50" : "cursor-pointer"}`}
            style={{
                width: "200px",
                margin: "10px",
                cursor: isOutOfStock ? "not-allowed" : "pointer"
            }}
            onClick={handleClick}
        >
            <img
                src={`/images/${product.name.toLowerCase()}.jpg`}
                alt={product.name}
                className="card-img-top"
                style={{
                    objectFit: "cover",
                    height: "150px"
                }}
            />
            <div className="card-body d-flex flex-column justify-content-between">
                <h5 className="card-title">{product.name}</h5>
                <p className="card-text">{"\u20AC"}{product.price.toFixed(2)}</p>
                <p className="card-text">In stock: {product.quantity}</p>
            </div>
        </div>

    );
};

export default ProductCard;
