import { AddressModel } from "./address-model";
import { OrderItemCreateModel } from "./orderItemCreate-model";

export class OrderCreateModel {
    constructor(
        public userId: number,
        public items: OrderItemCreateModel[],
        public deliveryAddress: AddressModel,
        public totalPrice: number,
        public paymentMode: string
    ) { }
}
