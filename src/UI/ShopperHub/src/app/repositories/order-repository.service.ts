import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { CancelOrderModel } from 'src/models/cancelOrder-model';
import { CancelledOrderModel } from 'src/models/cancelledOrder-model';
import { OrderModel } from 'src/models/order-model';
import { OrderCreateModel } from 'src/models/orderCreate-model';
import { RefundRequestModel } from 'src/models/refundReq-model';
import { RefundedOrderModel } from 'src/models/refundedOrder-model';

@Injectable({
  providedIn: 'root'
})
export class OrderRepositoryService {

  constructor(private httpClient: HttpClient) { }

  AddOrder(order: OrderCreateModel): Observable<boolean> {
    const header = { 'content-type': 'application/json' };

    var url: string = "https://localhost:3009/api/Order/AddOrder";

    return this.httpClient.post<boolean>(url, order, { headers: header })
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        })
      );
  }

  GetOrderById(id: number): Observable<OrderModel> {
    var url: string = "https://localhost:3009/api/Order/" + id;

    return this.httpClient.get<OrderModel>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        }),
        tap(responseData => {
        })
      );
  }

  GetOrdersByUserId(userId: number): Observable<OrderModel[]> {
    var url: string = "https://localhost:3009/api/Order/ByUserId/" + userId;

    return this.httpClient.get<OrderModel[]>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        }),
        tap(responseData => {
        })
      );
  }

  CancelOrder(cancelOrder: CancelOrderModel): Observable<string> {
    const header = { 'content-type': 'application/json' };

    var url: string = "https://localhost:3009/api/Order/CancelOrder";

    return this.httpClient.post<string>(url, cancelOrder, { headers: header })
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        })
      );
  }

  RefundOrder(req: RefundRequestModel): Observable<string> {
    const header = { 'content-type': 'application/json' };

    var url: string = "https://localhost:3009/api/Order/RefundOrder";

    return this.httpClient.post<string>(url, req, { headers: header })
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        })
      );
  }

  GetCancelledOrders(userId: number): Observable<CancelledOrderModel[]> {
    var url: string = "https://localhost:3009/api/Order/GetCancelledOrders/" + userId;

    return this.httpClient.get<CancelledOrderModel[]>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        }),
        tap(responseData => {
          //console.log(responseData);
        })
      );
  }

  GetRefundedOrders(userId: number): Observable<RefundedOrderModel[]> {
    var url: string = "https://localhost:3009/api/Order/GetRefundedOrders/" + userId;

    return this.httpClient.get<RefundedOrderModel[]>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        }),
        tap(responseData => {
        })
      );
  }
}
