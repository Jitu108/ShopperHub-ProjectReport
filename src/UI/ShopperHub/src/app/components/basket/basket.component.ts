import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { BasketInfo } from 'src/models/basket-info';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  itemCount: number;
  totalAmount: number;
  @Input() basketInfo: BasketInfo;
  @Output() onBasketClear = new EventEmitter();

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onBasketClearClick() {
    this.onBasketClear.emit();
  }

  checkout() {
    this.router.navigate(['/checkout'])
  }
}