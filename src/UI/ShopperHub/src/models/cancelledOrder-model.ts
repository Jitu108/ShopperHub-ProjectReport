export class CancelledOrderModel {
    constructor(
        public orderId: number,
        public cancellationDate: Date,
        public cancellationReason: string
    ) { }
}