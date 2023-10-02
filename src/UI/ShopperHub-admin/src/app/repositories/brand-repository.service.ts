import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BrandRead } from '../models/brand-read';
import { tap } from 'rxjs/operators';
import { BrandUpdate } from '../models/brand-update';

@Injectable({
  providedIn: 'root'
})
export class BrandRepositoryService {

  constructor(private httpClient: HttpClient) { }

  save(brandObj: BrandUpdate): Observable<boolean> {
    if (brandObj.id == 0) {
      const body = { brand: brandObj.brand };
      const header = { 'content-type': 'application/json' };

      var url: string = "https://localhost:3008/api/CatalogBrand/Add";

      return this.httpClient.post<boolean>(url, body, { headers: header });
    }
    else {
      const body = brandObj;
      const header = { 'content-type': 'application/json' };

      var url: string = "https://localhost:3008/api/CatalogBrand/Update";

      return this.httpClient.put<boolean>(url, body, { headers: header });
    }

  }

  delete(brandId: number) {
    var url: string = "https://localhost:3008/api/CatalogBrand/Delete/" + brandId;
    return this.httpClient.delete<boolean>(url);
  }

  getAll(): Observable<BrandRead[]> {
    var url: string = "https://localhost:3008/api/CatalogBrand/GetAll"
    return this.httpClient.get<BrandRead[]>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        }),
        tap(responseData => {
          //console.log(responseData);
        })
      );
  }

  getById(id: number): Observable<BrandRead> {
    var url: string = "https://localhost:3008/api/CatalogBrand/ById/" + id;
    return this.httpClient.get<BrandRead>(url);
  }
}
