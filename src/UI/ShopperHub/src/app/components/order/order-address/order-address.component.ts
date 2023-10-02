import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BasketService } from 'src/app/services/basket.service';
import { OrderService } from 'src/app/services/order.service';
import { AddressModel } from 'src/models/address-model';
import { OrderCreateModel } from 'src/models/orderCreate-model';
import { OrderItemCreateModel } from 'src/models/orderItemCreate-model';
import { ShoppingCart } from 'src/models/shopping-cart';

@Component({
  selector: 'app-order-address',
  templateUrl: './order-address.component.html',
  styleUrls: ['./order-address.component.scss']
})
export class OrderAddressComponent implements OnInit {

  cart: ShoppingCart;
  constructor(private orderService: OrderService, private basketService: BasketService, private router: Router) {
    this.cart = this.router.getCurrentNavigation().extras.state.cart;
  }

  public isLoading: boolean = false;
  public error: { hasError: boolean, message: string } = { hasError: false, message: "" };
  address: AddressModel = {} as AddressModel;
  cardType: string = "COD";

  ngOnInit(): void {
  }

  orderCreate: OrderCreateModel = {} as OrderCreateModel;
  orderItems: OrderItemCreateModel[];

  onSaveAddressClick(form: any) {
    this.prepareOrderToSave();
    this.orderService.AddOrder(this.orderCreate).subscribe(x => {
      this.basketService.DeleteBasket(this.cart.userId).subscribe(y => {
        this.router.navigate(['/order-placed']);
      });
    });
  }

  prepareOrderToSave() {
    var orderItems = this.cart.items.map(x => (new OrderItemCreateModel(x.productId, x.productName, x.unitPrice, x.quantity)));

    this.orderCreate.items = orderItems;
    this.orderCreate.deliveryAddress = this.address;
    this.orderCreate.paymentMode = "COD";
    this.orderCreate.totalPrice = orderItems.reduce((sum, current) => sum + current.unitPrice * current.quantity, 0);
    this.orderCreate.userId = this.cart.userId;
  }

  onBackClick() {
    this.router.navigate(['/checkout']);
  }
  onCancelClick() {
    this.router.navigate(['/checkout']);
  }
}
