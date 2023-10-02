import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IdentityService } from 'src/app/services/identity.service';
import { AuthUserModel } from 'src/models/authUser-model';

@Component({
  selector: 'top-header',
  templateUrl: './top-header.component.html',
  styleUrls: ['./top-header.component.scss']
})
export class TopHeaderComponent implements OnInit {

  private userSub: Subscription;
  authUser: AuthUserModel;

  constructor(private identityService: IdentityService, private router: Router) { }


  ngOnInit(): void {
    this.userSub = this.identityService.loggedInUser$.subscribe(x => {
      this.authUser = x;
    });
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }
  gotOrders() {
    this.router.navigate(['/order-list']);
  }
  logout() {
    this.identityService.logout();
    this.router.navigate(['/login'])
  }

}
