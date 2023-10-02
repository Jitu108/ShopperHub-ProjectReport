export class OrderItemReadModel {
    constructor(
        public id: number,
        public productId: number,
        public productName: string,
        public unitPrice: number,
        public quantity: number
    ) { }
}

export class OrderItemReadWithTotal {
    constructor(
        id: number,
        productId: number,
        productName: string,
        unitPrice: number,
        quantity: number
    ) {
        this.id = id;
        this.productId = productId;
        this.productName = productName;
        this.unitPrice = unitPrice;
        this.quantity = quantity;
        this.totalPrice = this.unitPrice * this.quantity;
    }

    public id: number;
    public productId: number;
    public productName: string;
    public unitPrice: number;
    public quantity: number;
    public totalPrice: number;
}