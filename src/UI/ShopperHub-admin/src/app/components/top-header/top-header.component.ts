import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthUserModel } from 'src/app/models/authUser-model';
import { IdentityService } from 'src/app/services/identity.service';

@Component({
  selector: 'top-header',
  templateUrl: './top-header.component.html',
  styleUrls: ['./top-header.component.scss']
})
export class TopHeaderComponent implements OnInit, OnDestroy {

  private userSub: Subscription;
  public isSelected: boolean = false;

  constructor(private identityService: IdentityService, private router: Router) { }

  authUser: AuthUserModel;

  ngOnInit(): void {
    this.userSub = this.identityService.loggedInUser$.subscribe(x => {
      this.authUser = x;

      if (this.authUser != null) {
        this.isSelected = true;
      }
    });
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }

  logout() {
    this.identityService.logout();
    this.router.navigate(['/login'])
  }

}
