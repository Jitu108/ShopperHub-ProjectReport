import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IdentityService } from 'src/app/services/identity.service';
import { AddUserModel } from 'src/models/addUser-model';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent implements OnInit {

  constructor(private identityService: IdentityService, private router: Router) { }
  public error: { hasError: boolean, message: string } = { hasError: false, message: "" };
  register: AddUserModel = {} as AddUserModel;
  ngOnInit(): void {
  }

  onRegisterClick() {
    this.identityService.RegisterUser(this.register).subscribe(x => {
      this.router.navigate(['/login']);
    });
  }

  onCancelClick() {
    this.router.navigate(['/login']);
  }
}
