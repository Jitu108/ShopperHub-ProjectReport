export class AddressModel {
    constructor(
        public id: number,
        public firstName: string,
        public lastName: string,
        public addressLine1: string,
        public addressLine2: string,
        public city: string,
        public state: string,
        public pin: string
    ) { }
}