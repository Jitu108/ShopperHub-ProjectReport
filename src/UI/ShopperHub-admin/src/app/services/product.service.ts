import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { createTextChangeRange } from 'typescript';
import { ProductCreate } from '../models/product-create';
import { ProductRead } from '../models/product-read';
import { ProductRepositoryService } from '../repositories/product-repository.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private repo: ProductRepositoryService) { }

  save(product: ProductCreate): Observable<boolean> {
    return this.repo.save(product);
  }

  getById(productId: number): Observable<ProductRead> {
    return this.repo.getById(productId);
  }

  getAll(): Observable<ProductRead[]> {
    return this.repo.getAll();
  }

  getByBrand(brandId: number): Observable<ProductRead[]> {
    return this.repo.getByBrand(brandId);
  }

  getByType(typeId: number): Observable<ProductRead[]> {
    return this.repo.getByType(typeId);
  }

  delete(productId: number): Observable<boolean> {
    return this.repo.delete(productId);
  }

  getProductCreateByReadObject(read: ProductRead): ProductCreate {
    var create = new ProductCreate();
    create.id = read.id;
    create.name = read.name;
    create.description = read.description;
    create.price = read.price;
    create.mrp = read.mrp;
    create.catalogTypeId = read.catalogTypeId;
    create.catalogBrandId = read.catalogBrandId;
    create.availableStock = read.availableStock;
    create.restockThreshold = read.restockThreshold;
    create.maxStockThreshold = read.maxStockThreshold;
    create.imageId = read.imageId;
    create.imageName = read.imageName;
    create.imageCaption = read.imageCaption;
    create.imageData = read.imageData;

    return create;
  }
}
