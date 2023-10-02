import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { TypeRead } from 'src/app/models/type-read';
import { TypeService } from 'src/app/services/type.service';

@Component({
  selector: 'app-catalog-type-list',
  templateUrl: './catalog-type-list.component.html',
  styleUrls: ['./catalog-type-list.component.scss']
})
export class CatalogTypeListComponent implements OnInit {

  displayColumns: string[] = ['id', 'type', 'action'];
  dataSource = new MatTableDataSource<TypeRead>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(
    private typeService: TypeService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getAll();
  }

  addType() {
    this.router.navigate(['/types/detail']);
  }

  editType(id: number) {
    this.router.navigate(['/types/detail'], { queryParams: { id: id } });
  }

  deleteType(id: number) {
    this.typeService.delete(id)
      .subscribe(x => {
        this.getAll();
      })
  }

  getAll() {
    this.typeService.getAll()
      .subscribe(x => {
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
