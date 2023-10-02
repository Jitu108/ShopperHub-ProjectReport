import { Injectable } from '@angular/core';
import { OrderRepositoryService } from '../repositories/order-repository.service';
import { OrderCreateModel } from 'src/models/orderCreate-model';
import { Observable } from 'rxjs';
import { OrderModel } from 'src/models/order-model';
import { CancelOrderModel } from 'src/models/cancelOrder-model';
import { RefundedOrderModel } from 'src/models/refundedOrder-model';
import { RefundRequestModel } from 'src/models/refundReq-model';
import { CancelledOrderModel } from 'src/models/cancelledOrder-model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private repo: OrderRepositoryService) { }

  AddOrder(order: OrderCreateModel): Observable<boolean> {
    return this.repo.AddOrder(order);
  }

  GetOrderById(id: number): Observable<OrderModel> {
    return this.repo.GetOrderById(id);
  }

  GetOrdersByUserId(userId: number): Observable<OrderModel[]> {
    return this.repo.GetOrdersByUserId(userId);
  }

  CancelOrder(cancelOrder: CancelOrderModel): Observable<string> {
    return this.repo.CancelOrder(cancelOrder);
  }

  RefundOrder(req: RefundRequestModel): Observable<string> {
    return this.repo.RefundOrder(req);
  }

  GetCancelledOrders(userId: number): Observable<CancelledOrderModel[]> {
    return this.repo.GetCancelledOrders(userId);
  }
  GetRefundedOrders(userId: number): Observable<RefundedOrderModel[]> {
    return this.repo.GetRefundedOrders(userId);
  }

}
