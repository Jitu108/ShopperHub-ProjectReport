export class RefundedOrderModel {
    constructor(
        public orderId: number,
        public refundDate: Date,
        public refundedAmount: number
    ) { }
}