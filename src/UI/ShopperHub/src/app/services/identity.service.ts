import { Injectable } from '@angular/core';
import { IdentityRepositoryService } from '../repositories/identity-repository.service';
import { LoginModel } from 'src/models/login-model';
import { Observable, ReplaySubject } from 'rxjs';
import { AuthUserModel } from 'src/models/authUser-model';
import { AddUserModel } from 'src/models/addUser-model';
import { UserModel } from 'src/models/user-model';
import { catchError, map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  private subject = new ReplaySubject<AuthUserModel>();
  loggedInUser$: Observable<AuthUserModel> = this.subject.asObservable();

  constructor(private identityRepo: IdentityRepositoryService, private router: Router) { }

  login = (loginModel: LoginModel): Observable<boolean> => {
    return this.identityRepo.Login(loginModel)
      .pipe(
        map((x: AuthUserModel) => {
          if (x != null) {
            this.subject.next(x);
            return true;
          }
          else {
            return false;
          }
        }),
        catchError(err => { throw (err) })
      );
  }

  RegisterUser(addUser: AddUserModel): Observable<boolean> {
    return this.identityRepo.RegisterUser(addUser);
  }

  getUserByEmail(email: string): Observable<UserModel> {
    return this.identityRepo.getUserByEmail(email);
  }

  getUserById(id: number): Observable<UserModel> {
    return this.identityRepo.getUserById(id);
  }

  logout = () => {
    this.subject.next(null);
    this.router.navigate(['/login']);
  }
}
