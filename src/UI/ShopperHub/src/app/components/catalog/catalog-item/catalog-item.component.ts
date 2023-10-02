import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product, ProductType } from 'src/models/product';

@Component({
  selector: 'catalog-item',
  templateUrl: './catalog-item.component.html',
  styleUrls: ['./catalog-item.component.scss']
})
export class CatalogItemComponent implements OnInit {

  discountStr: string;
  constructor() { }

  @Input() product: ProductType;
  @Input() id: number;
  @Output() onCountDecrease = new EventEmitter<number>();
  @Output() onCountIncrease = new EventEmitter<number>();


  ngOnInit(): void {
    var discountRs = this.product.mrp - this.product.price;
    var d = discountRs * 100 / this.product.mrp;
    this.discountStr = d.toFixed(2);
  }

  onCountDecreaseClick(id) {
    this.onCountDecrease.emit(id);
  }

  onCountIncreaseClick(id) {
    this.onCountIncrease.emit(id);
  }

  getDiscount() {
    var discount = this.product.mrp - this.product.price;
    var discountPercent = discount * 100 / this.product.mrp;
    return discountPercent;
  }

}
