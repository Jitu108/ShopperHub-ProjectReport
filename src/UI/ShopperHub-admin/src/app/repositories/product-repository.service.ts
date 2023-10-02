import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductCreate } from '../models/product-create';
import { ProductRead } from '../models/product-read';

@Injectable({
  providedIn: 'root'
})
export class ProductRepositoryService {

  constructor(private httpClient: HttpClient) { }

  save(productObj: ProductCreate): Observable<boolean> {
    const body = productObj;
    if (productObj.id == 0 || productObj.id === undefined) {
      const header = { 'content-type': 'application/json' };
      var url: string = "https://localhost:3008/api/CatalogProduct/Add";
      return this.httpClient.post<boolean>(url, body, { headers: header });
    }
    else {
      const header = { 'content-type': 'application/json' };
      var url: string = "https://localhost:3008/api/CatalogProduct/Update/" + productObj.id;
      return this.httpClient.put<boolean>(url, body, { headers: header });
    }

  }

  getById(productId: number): Observable<ProductRead> {
    var url: string = "https://localhost:3008/api/CatalogProduct/ById/" + productId;
    return this.httpClient.get<ProductRead>(url);
  }

  getAll(): Observable<ProductRead[]> {
    var url: string = "https://localhost:3008/api/CatalogProduct/GetAll";
    return this.httpClient.get<ProductRead[]>(url);
  }

  getByBrand(brandId: number): Observable<ProductRead[]> {
    var url: string = "https://localhost:3008/api/CatalogProduct/ByBrand/" + brandId;
    return this.httpClient.get<ProductRead[]>(url);
  }

  getByType(typeId: number): Observable<ProductRead[]> {
    var url: string = "https://localhost:3008/api/CatalogProduct/ByType/" + typeId;
    return this.httpClient.get<ProductRead[]>(url);
  }

  delete(productId: number): Observable<boolean> {
    var url: string = "https://localhost:3008/api/CatalogProduct/Delete/" + productId;
    return this.httpClient.delete<boolean>(url);
  }
}
