import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IdentityService } from 'src/app/services/identity.service';
import { LoginModel } from 'src/models/login-model';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.scss']
})
export class UserLoginComponent implements OnInit {

  public error: { hasError: boolean, message: string } = { hasError: false, message: "" };

  Email: string = 'gupta.jitendra108@gmail.com';
  Password: string = 'test';
  constructor(private identityService: IdentityService, private router: Router) { }

  ngOnInit(): void {
    this.error.hasError = false;
  }

  // Submit
  onLoginClick(form: any) {
    if (form.valid) {
      this.error.hasError = false;

      this.identityService.login(new LoginModel(this.Email, this.Password))
        .subscribe(res => {
          this.router.navigate(['/products']);
        }), (error: any) => {
          this.error = { hasError: true, message: error.message };
        }
    }
  }

  // Cancel
  onCancelClick() {
    this.error = { hasError: false, message: "" };
  }
}
