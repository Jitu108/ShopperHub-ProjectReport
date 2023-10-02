import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Product } from 'src/models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductRepositoryService {

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<Product[]> {
    var url: string = "https://localhost:3009/api/CatalogProduct/GetAll"

    return this.httpClient.get<Product[]>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        }),
        tap(responseData => {
        })
      );
  }
}
