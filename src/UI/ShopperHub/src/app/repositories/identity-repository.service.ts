import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AddUserModel } from 'src/models/addUser-model';
import { AuthUserModel } from 'src/models/authUser-model';
import { LoginModel } from 'src/models/login-model';
import { UserModel } from 'src/models/user-model';

@Injectable({
  providedIn: 'root'
})
export class IdentityRepositoryService {

  constructor(private httpClient: HttpClient) { }

  Login(loginModel: LoginModel): Observable<AuthUserModel> {
    const header = { 'content-type': 'application/json' };

    var url: string = "https://localhost:3009/api/Identity/Login";

    return this.httpClient.post<AuthUserModel>(url, loginModel, { headers: header })
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        })
      );
  }

  RegisterUser(addUser: AddUserModel): Observable<boolean> {
    const header = { 'content-type': 'application/json' };

    var url: string = "https://localhost:3009/api/Identity/Register";

    return this.httpClient.post<boolean>(url, addUser, { headers: header })
      .pipe(
        catchError(errorResponse => {
          return throwError(errorResponse.error);
        })
      );
  }

  getUserByEmail(email: string): Observable<UserModel> {
    var url: string = "https://localhost:3009/api/Identity/ByEmail/" + email;

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
    var url: string = "https://localhost:3009/api/Identity/ById/" + id;

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
