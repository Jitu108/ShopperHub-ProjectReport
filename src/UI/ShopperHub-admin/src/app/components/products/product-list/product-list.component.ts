import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ProductRead } from 'src/app/models/product-read';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  displayColumns: string[] = ['image', 'id', 'name', 'description', 'price', 'mrp', 'catalogType', 'catalogBrand', 'availableStock', 'action'];
  dataSource = new MatTableDataSource<ProductRead>();
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(
    private productService: ProductService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.productService
      .getAll()
      .subscribe(x => {
        x.forEach(y => y.toReplenish = y.availableStock < y.restockThreshold);
        this.dataSource.data = x;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      });
  }

  addProduct() {
    this.router.navigate(['products/detail']);
  }

  editProduct(id: number) {
    this.router.navigate(['products/detail'], { queryParams: { id: id } });
  }

  reStock(id: number) {
    this.router.navigate(['products/restock'], { queryParams: { id: id } });
  }

  deleteProduct(id: number) {
    this.productService.delete(id).subscribe(x => {
      this.getAll();
    })
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}
