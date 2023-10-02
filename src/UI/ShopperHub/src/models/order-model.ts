import { AddressModel } from "./address-model";
import { OrderItemReadModel } from "./orderItemRead-model";

export class OrderModel {
    constructor(
        public id: number,
        public userId: number,
        public items: OrderItemReadModel[],
        public totalPrice: number,
        public orderDate: Date,
        public deliveryAddress: AddressModel,
        public paymentMode: string,
        public orderStatus: string
    ) { }
}