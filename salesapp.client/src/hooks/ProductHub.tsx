import { useEffect } from "react";
import * as signalR from "@microsoft/signalr";
import type { Product } from "../App";

interface UseProductHubProps {
    onProductUpdated: (product: Product) => void;
}

export default function useProductHub({ onProductUpdated } : UseProductHubProps) {
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

        return () => {
            connection.stop();
        };
    }, [onProductUpdated]);
}
