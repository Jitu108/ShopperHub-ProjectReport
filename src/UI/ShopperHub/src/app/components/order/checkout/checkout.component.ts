import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BasketService } from 'src/app/services/basket.service';
import { IdentityService } from 'src/app/services/identity.service';
import { AuthUserModel } from 'src/models/authUser-model';
import { BasketInfo } from 'src/models/basket-info';
import { BasketRead } from 'src/models/basket-read';
import { ShoppingCart } from 'src/models/shopping-cart';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  displayColumns: string[] = ['productName', 'unitPrice', 'quantity', 'totalPrice'];
  dataSource = new MatTableDataSource<BasketRead>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  private userSub: Subscription;
  authUser: AuthUserModel;

  shoppingCart = new ShoppingCart();
  basketInfo = {} as BasketInfo;

  constructor(private identityService: IdentityService, private basketService: BasketService, private router: Router) { }

  ngOnInit(): void {
    this.userSub = this.identityService.loggedInUser$.subscribe(x => {
      this.authUser = x;
      this.getBasket();
    });
  }

  getBasket() {
    this.basketService.GetBasket(this.authUser.id).subscribe(x => {

      var data = x.items.map(x => (new BasketRead(x.productId, x.productName, x.unitPrice, x.quantity)));
      data.forEach(x => x.totalPrice = x.quantity * x.unitPrice);

      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;

      this.shoppingCart = x;
      this.basketInfo.totalAmount = x.items.reduce((sum, current) => sum + current.unitPrice * current.quantity, 0);
      this.basketInfo.itemCount = x.items.reduce((sum, current) => sum + current.quantity, 0);
    })
  }

  applyfilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  placeorder() {
    this.router.navigate(['/order-address'], { state: { cart: this.shoppingCart } })
  }

  onBackClick() {
    this.router.navigate(['/products']);
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }

}
