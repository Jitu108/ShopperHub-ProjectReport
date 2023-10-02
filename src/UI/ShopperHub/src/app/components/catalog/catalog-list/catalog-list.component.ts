import { Component, OnInit } from '@angular/core';
import { BasketInfo } from 'src/models/basket-info';
import { Product, ProductType } from 'src/models/product';
import { ProductService } from 'src/app/services/product.service';
import { ShoppingCart } from 'src/models/shopping-cart';
import { CartItem } from 'src/models/cart-item';
import { BasketService } from 'src/app/services/basket.service';
import { IdentityService } from 'src/app/services/identity.service';
import { Subscription } from 'rxjs';
import { AuthUserModel } from 'src/models/authUser-model';

@Component({
  selector: 'app-catalog-list',
  templateUrl: './catalog-list.component.html',
  styleUrls: ['./catalog-list.component.scss']
})
export class CatalogListComponent implements OnInit {

  constructor(private productService: ProductService, private basketService: BasketService, private identityService: IdentityService) { }
  title = "Angular Grid Card View";
  baseProducts: Product[] = [];
  products: ProductType[] = [];
  basketInfo = {} as BasketInfo;
  shoppingCart = new ShoppingCart();
  private userSub: Subscription;
  authUser: AuthUserModel;
  ngOnInit(): void {
    this.userSub = this.identityService.loggedInUser$.subscribe(x => {
      this.authUser = x;

      this.basketInfo.itemCount = 0;
      this.basketInfo.totalAmount = 0;
      this.getBasket();
    });


  }

  getBasket() {
    this.basketService.GetBasket(this.authUser.id).subscribe(x => {
      this.shoppingCart = x;
      this.basketInfo.totalAmount = x.items.reduce((sum, current) => sum + current.unitPrice * current.quantity, 0);
      this.basketInfo.itemCount = x.items.reduce((sum, current) => sum + current.quantity, 0);
      this.getAll();
    })
  }

  getAll() {
    this.productService.getAll().subscribe(x => {
      this.baseProducts = x;
      this.products = this.baseProducts.map(x => {

        var item = this.shoppingCart.items.find(y => y.productId == x.id);
        var quantity = 0;
        if (item != undefined && item != null) {
          quantity = item.quantity;
        }
        return this.productService.getTypeFromProduct(
          x.id,
          x.name,
          x.description,
          x.price,
          x.mrp,
          x.availableStock,
          x.imageName,
          x.imageCaption,
          x.imageData,
          quantity)
      }
      );
    });
  }

  onCountDecreaseClick(id: number) {
    var product = this.products.find(x => x.id == id);
    if (product.count != 0) {
      {
        this.products.find(x => x.id == id).count = product.count - 1;

        this.basketInfo.itemCount--;
        this.basketInfo.totalAmount -= product.price;

        this.updateShoppingCart(id, product.name, product.price, product.count);
      }
    }
  }

  onCountIncreaseClick(id: number) {
    var product = this.products.find(x => x.id == id);
    if (product.count < product.availableStock) {
      this.products.find(x => x.id == id).count = product.count + 1;

      this.basketInfo.itemCount++;
      this.basketInfo.totalAmount += product.price;

      this.updateShoppingCart(id, product.name, product.price, product.count);
    }
  }
  updateShoppingCart(productId: number, productName: string, unitPrice: number, quantity: number) {
    if (this.shoppingCart.userId == 0) {
      this.shoppingCart.userId = this.authUser.id;
    }
    var cartItem = new CartItem(productId, productName, unitPrice, quantity);

    var existingCartItem = this.shoppingCart.items.find(x => x.productId == productId);
    if (existingCartItem == null) {
      this.shoppingCart.items.push(cartItem);
    }
    else {
      existingCartItem.quantity = quantity;
    }

    this.shoppingCart.totalPrice = this.shoppingCart.items.reduce((sum, current) => sum + current.unitPrice * current.quantity, 0);
    this.basketService.UpdateBasket(this.shoppingCart).subscribe();
  }

  clearBasket() {
    this.basketInfo.itemCount = 0;
    this.basketInfo.totalAmount = 0;
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }

  onBasketClearClick() {
    this.basketService.DeleteBasket(this.authUser.id).subscribe(x => {
      this.clearBasket();
      this.getBasket();
    });

  }
}
