import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel } from '../models/login-model';
import { AuthUserModel } from '../models/authUser-model';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { UserModel } from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class IdentityRepositoryService {

  constructor(private httpClient: HttpClient) { }

  Login(loginModel: LoginModel): Observable<AuthUserModel> {
    const header = { 'content-type': 'application/json' };

    var url: string = "https://localhost:3008/api/Identity/Login";

    return this.httpClient.post<AuthUserModel>(url, loginModel, { headers: header })
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        })
      );
  }

  getUserByEmail(email: string): Observable<UserModel> {
    var url: string = "https://localhost:3008/api/Identity/ByEmail/" + email;

    return this.httpClient.get<UserModel>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        }),
        tap(responseData => {
        })
      );
  }

  getUserById(id: number): Observable<UserModel> {
    var url: string = "https://localhost:3008/api/Identity/ById/" + id;

    return this.httpClient.get<UserModel>(url)
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        }),
        tap(responseData => {
        })
      );
  }
}
