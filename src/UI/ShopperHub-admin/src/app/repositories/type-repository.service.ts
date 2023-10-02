import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { TypeRead } from '../models/type-read';
import { TypeUpdate } from '../models/type-update';

@Injectable({
  providedIn: 'root'
})
export class TypeRepositoryService {

  constructor(private httpClient: HttpClient) { }

  save(typeObj: TypeUpdate): Observable<boolean> {
    if (typeObj.id == 0) {
      const body = { type: typeObj.type };
      const header = { 'content-type': 'application/json' };

      var url: string = "https://localhost:3008/api/CatalogType/Add";

      return this.httpClient.post<boolean>(url, body, { headers: header });
    }
    else {
      const body = typeObj;
      const header = { 'content-type': 'application/json' };

      var url: string = "https://localhost:3008/api/CatalogType/Update";

      return this.httpClient.put<boolean>(url, body, { headers: header });
    }
  }

  delete(typeId: number) {
    var url: string = "https://localhost:3008/api/CatalogType/Delete/" + typeId;
    return this.httpClient.delete<boolean>(url);
  }

  getAll(): Observable<TypeRead[]> {
    var url: string = "https://localhost:3008/api/CatalogType/GetAll";
    return this.httpClient.get<TypeRead[]>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        }),
        tap(responseData => {
        })
      );
  }

  getById(id: number): Observable<TypeRead> {
    var url: string = "https://localhost:3008/api/CatalogType/ById/" + id;
    return this.httpClient.get<TypeRead>(url);
  }
}
