export class OrderItemCreateModel {
    constructor(
        public productId: number,
        public productName: string,
        public unitPrice: number,
        public quantity: number
    ) { }
}