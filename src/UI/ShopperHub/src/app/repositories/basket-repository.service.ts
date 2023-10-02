import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ShoppingCart } from 'src/models/shopping-cart';

@Injectable({
  providedIn: 'root'
})
export class BasketRepositoryService {

  constructor(private httpClient: HttpClient) { }

  GetBasket(userId: number): Observable<ShoppingCart> {
    var url: string = "https://localhost:3009/api/Basket/" + userId;

    return this.httpClient.get<ShoppingCart>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        }),
        tap(responseData => {
        })
      );
  }

  UpdateBasket(cart: ShoppingCart): Observable<boolean> {
    //const body = { cart: cart };
    const header = { 'content-type': 'application/json' };

    var url: string = "https://localhost:3009/api/Basket";

    return this.httpClient.post<boolean>(url, cart, { headers: header })
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        })
      );
  }

  DeleteBasket(userId: number): Observable<boolean> {
    var url: string = "https://localhost:3009/api/Basket/" + userId;

    return this.httpClient.delete<boolean>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        })
      );
  }
}
