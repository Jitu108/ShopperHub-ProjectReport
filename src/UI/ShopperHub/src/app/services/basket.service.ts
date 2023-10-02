import { Injectable } from '@angular/core';
import { BasketRepositoryService } from '../repositories/basket-repository.service';
import { Observable } from 'rxjs';
import { ShoppingCart } from 'src/models/shopping-cart';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(private repo: BasketRepositoryService) { }

  GetBasket(userId: number): Observable<ShoppingCart> {
    return this.repo.GetBasket(userId);
  }

  UpdateBasket(cart: ShoppingCart): Observable<boolean> {
    return this.repo.UpdateBasket(cart);
  }

  DeleteBasket(userId: number): Observable<boolean> {
    return this.repo.DeleteBasket(userId);
  }
}
