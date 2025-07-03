import { useEffect } from "react";
import * as signalR from "@microsoft/signalr";
import type { Product } from "../App";

interface UseProductHubProps {
    onProductUpdated: (product: Product) => void;
    onProductReserved: (id: string, quantity: number) => void;
}

export default function useProductHub({ onProductUpdated, onProductReserved }: UseProductHubProps) {
    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("/hubs/products")
            .withAutomaticReconnect()
            .build();

        connection.start().catch(console.error);

        connection.on("ProductUpdated", (product: Product) => {
            console.log("Product updated:", product);
            onProductUpdated(product);
        });

        connection.on("ProductReserved", (data) => {
            console.log("Product reserved:", data);
            onProductReserved(data.productId, data.quantity);
        });

        return () => {
            connection.stop();
        };
    }, [onProductUpdated, onProductReserved]);
}
