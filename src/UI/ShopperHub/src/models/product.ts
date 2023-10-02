export class Product {
    constructor(
        id: number,
        name: string,
        description: string,
        price: number,
        mrp: number,
        catalogTypeId: number,
        catalogType: string,
        catalogBrandId: number,
        catalogBrand: string,
        availableStock: number,
        restockThreshold: number,
        maxStockThreshold: number,
        imageId: number,
        imageName: string,
        imageCaption: string,
        imageData: string
    ) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.price = price;
        this.mrp = mrp;

        this.imageCaption = imageCaption;
        this.imageName = imageName;
        this.imageData = imageData;
    }

    id: number;
    name: string;
    description: string;
    price: number;
    mrp: number;
    catalogTypeId: number;
    catalogType: string;
    catalogBrandId: number;
    catalogBrand: string;
    availableStock: number;
    restockThreshold: number;
    maxStockThreshold: number;
    imageId: number;
    imageName: string;
    imageCaption: string;
    imageData: string;
}

export type ProductType = {

    id: number;
    name: string;
    description: string;
    price: number;
    mrp: number;
    imageName: string;
    imageCaption: string;
    imageData: string;
    availableStock: number;
    count: number;
}