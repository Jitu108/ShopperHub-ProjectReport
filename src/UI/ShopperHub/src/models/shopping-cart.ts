import { CartItem } from "./cart-item";

export class ShoppingCart {
    constructor(
        userId: number = 0,
        totalPrice: number = 0,
        items: CartItem[] = [],

    ) {
        this.userId = userId;
        this.totalPrice = totalPrice;
        this.items = items;
    }

    userId: number;
    totalPrice: number;
    items: CartItem[];
}