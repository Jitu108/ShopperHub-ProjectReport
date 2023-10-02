import { Product, ProductType } from "src/models/product";
import { ProductRepositoryService } from "../repositories/product-repository.service";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    constructor(private repo: ProductRepositoryService) { }
    getAll(): Observable<Product[]> {
        return this.repo.getAll();
    }

    getTypeFromProduct(
        id: number,
        name: string,
        description: string,
        price: number,
        mrp: number,
        availableStock: number,
        imageName: string,
        imageCaption: string,
        imageData: string,
        count: number

    ): ProductType {
        var t: ProductType = {
            id: id,
            name: name,
            description: description,
            price: price,
            mrp: mrp,
            count: count,
            availableStock: availableStock,
            imageName: imageName,
            imageCaption: imageCaption,
            imageData: imageData
        };
        return t;
    }
}