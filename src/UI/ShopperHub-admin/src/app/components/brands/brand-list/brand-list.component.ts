import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BrandRead } from 'src/app/models/brand-read';
import { BrandService } from 'src/app/services/brand.service';
import { isThisTypeNode } from 'typescript';

@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.scss']
})
export class BrandListComponent implements OnInit {

  displayColumns: string[] = ['id', 'brand', 'action'];
  dataSource = new MatTableDataSource<BrandRead>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private brandService: BrandService,
    private router: Router) { }

  ngOnInit(): void {
    this.getAll();
  }

  addBrand() {
    this.router.navigate(['/brands/detail'])
  }
  editBrand(id: number) {
    this.router.navigate(['/brands/detail'], { queryParams: { id: id } });
  }

  deleteBrand(id: number) {
    this.brandService.delete(id)
      .subscribe(x => {
        this.getAll();
      });

  }

  getAll() {
    this.brandService.getAll().subscribe(x => {
      this.dataSource.data = x;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyfilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}
