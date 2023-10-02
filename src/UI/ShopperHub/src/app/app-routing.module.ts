import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CatalogListComponent } from './components/catalog/catalog-list/catalog-list.component';
import { UserLoginComponent } from './components/user/user-login/user-login.component';
import { AuthGuard } from './services/auth.guard';
import { OrderListComponent } from './components/order/order-list/order-list.component';
import { CheckoutComponent } from './components/order/checkout/checkout.component';
import { OrderSuccessComponent } from './components/order/order-success/order-success.component';
import { OrderAddressComponent } from './components/order/order-address/order-address.component';
import { UserRegisterComponent } from './components/user/user-register/user-register.component';
import { OrderDetailComponent } from './components/order/order-detail/order-detail.component';

const routes: Routes = [
  { path: 'login', component: UserLoginComponent },
  { path: 'register', component: UserRegisterComponent },
  { path: 'products', component: CatalogListComponent, canActivate: [AuthGuard] },
  { path: 'checkout', component: CheckoutComponent, canActivate: [AuthGuard] },
  { path: 'order-placed', component: OrderSuccessComponent, canActivate: [AuthGuard] },
  { path: 'order-address', component: OrderAddressComponent, canActivate: [AuthGuard] },
  { path: 'order-detail', component: OrderDetailComponent, canActivate: [AuthGuard] },
  { path: 'order-list', component: OrderListComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: "not-found" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
