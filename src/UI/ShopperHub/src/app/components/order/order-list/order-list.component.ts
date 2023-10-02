import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IdentityService } from 'src/app/services/identity.service';
import { OrderService } from 'src/app/services/order.service';
import { AuthUserModel } from 'src/models/authUser-model';
import { CancelledOrderModel } from 'src/models/cancelledOrder-model';
import { OrderModel } from 'src/models/order-model';
import { OrderBasicModel } from 'src/models/orderBasic-model';
import { RefundedOrderModel } from 'src/models/refundedOrder-model';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit {

  displayColumns: string[] = ['id', 'totalPrice', 'orderDate', 'orderStatus', 'paymentMode', 'cancellationDate', 'cancellationReason', 'refundDate'];
  dataSource = new MatTableDataSource<OrderBasicModel>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  private userSub: Subscription;
  authUser: AuthUserModel;
  orders: OrderModel[] = [];
  cancelledOrders: CancelledOrderModel[] = [];
  refundedOrders: RefundedOrderModel[] = [];
  constructor(private identityService: IdentityService, private orderService: OrderService, private router: Router) { }

  ngOnInit(): void {
    this.userSub = this.identityService.loggedInUser$.subscribe(x => {
      this.authUser = x;
      this.getOrders();
    });
  }

  getOrders() {
    var userId = this.authUser.id;
    this.orderService.GetOrdersByUserId(userId).subscribe(x => {
      this.orders = x;
      this.orderService.GetCancelledOrders(userId).subscribe(x1 => {
        this.cancelledOrders = x1;
        this.orderService.GetRefundedOrders(userId).subscribe(x2 => {
          this.refundedOrders = x2;

          this.prepareData(x, x1, x2);
          // var data = x.map(y => (new OrderBasicModel(y.id, y.userId, y.totalPrice, y.orderDate, y.paymentMode, y.orderStatus)));

          // this.dataSource.data = data;
          // this.dataSource.paginator = this.paginator;
          // this.dataSource.sort = this.sort;


        });
      });

    });
  }

  prepareData(x: OrderModel[], x1: CancelledOrderModel[], x2: RefundedOrderModel[]) {
    var data = x.map(y => {
      var cancel = x1.find(y1 => y1.orderId == y.id);
      var refund = x2.find(y2 => y2.orderId == y.id);
      return (new OrderBasicModel(y.id, y.userId, y.totalPrice, y.orderDate, y.paymentMode, y.orderStatus,
        cancel != null ? cancel.cancellationReason : '',
        cancel != null ? cancel.cancellationDate : null,
        refund != null ? refund.refundDate : null
      ));
    });


    this.dataSource.data = data;
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  onOrderClick(row) {
    var order = this.orders.find(x => x.id == row.id);
    this.router.navigate(['/order-detail'], { state: { order: order } })
  }

  onBackClick() {
    this.router.navigate(['/products'])
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }
}
