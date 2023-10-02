import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import { CancelOrderModel } from 'src/models/cancelOrder-model';
import { OrderModel } from 'src/models/order-model';
import { OrderItemReadModel, OrderItemReadWithTotal } from 'src/models/orderItemRead-model';
import { RefundRequestModel } from 'src/models/refundReq-model';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit {

  displayColumns: string[] = ['productName', 'unitPrice', 'quantity', 'totalPrice'];
  dataSource = new MatTableDataSource<OrderItemReadWithTotal>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  order: OrderModel;
  constructor(private orderService: OrderService, private router: Router) {
    this.order = this.router.getCurrentNavigation().extras.state.order;
  }

  ngOnInit(): void {
    this.prepareDataSource();
  }

  prepareDataSource() {
    this.dataSource.data = this.order.items.map(x => (new OrderItemReadWithTotal(x.id, x.productId, x.productName, x.unitPrice, x.quantity)));
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  cancelOrder(orderId: number) {
    var cancelOrder = new CancelOrderModel(orderId, 'Order not delivered on time');
    this.orderService.CancelOrder(cancelOrder).subscribe(x => {
      this.router.navigate(['/order-list'])
    });
  }
  requestRefund(orderId: number) {
    var req = new RefundRequestModel(orderId);
    this.orderService.RefundOrder(req).subscribe(x => {
      this.router.navigate(['/order-list'])
    });
  }
  onBackClick() {
    this.router.navigate(['/order-list'])
  }

}
