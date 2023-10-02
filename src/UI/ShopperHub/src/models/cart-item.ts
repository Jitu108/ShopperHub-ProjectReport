export class CartItem {
    constructor(
        productId: number,
        productName: string,
        unitPrice: number,
        quantity: number,

    ) {
        this.productId = productId;
        this.productName = productName;
        this.unitPrice = unitPrice;
        this.quantity = quantity;
    }

    productId: number;
    productName: string;
    unitPrice: number;
    quantity: number;
}