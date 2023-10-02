export class CancelOrderModel {
    constructor(
        public orderId: number,
        public cancellationReason: string
    ) { }
}