export class OrderBasicModel {
    constructor(
        public id: number,
        public userId: number,
        public totalPrice: number,
        public orderDate: Date,
        public paymentMode: string,
        public orderStatus: string,
        public cancellationReason: string,
        public cancellationDate?: Date,
        public refundDate?: Date
    ) { }
}