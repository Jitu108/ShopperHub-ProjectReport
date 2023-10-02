import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CatalogListComponent } from './components/catalog/catalog-list/catalog-list.component';
import { TopHeaderComponent } from './components/top-header/top-header.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatCardModule } from "@angular/material/card";
import { MatCommonModule } from '@angular/material/core';
import { MatComponentsModule } from './modules/mat-components/mat-components.module';
import { CatalogItemComponent } from './components/catalog/catalog-item/catalog-item.component';
import { BasketComponent } from './components/basket/basket.component';
import { OrderListComponent } from './components/order/order-list/order-list.component';
import { UserLoginComponent } from './components/user/user-login/user-login.component';
import { CheckoutComponent } from './components/order/checkout/checkout.component';
import { OrderSuccessComponent } from './components/order/order-success/order-success.component';
import { OrderAddressComponent } from './components/order/order-address/order-address.component';
import { UserRegisterComponent } from './components/user/user-register/user-register.component';
import { OrderDetailComponent } from './components/order/order-detail/order-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    CatalogListComponent,
    TopHeaderComponent,
    CatalogItemComponent,
    BasketComponent,
    OrderListComponent,
    UserLoginComponent,
    CheckoutComponent,
    OrderSuccessComponent,
    OrderAddressComponent,
    UserRegisterComponent,
    OrderDetailComponent,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatCommonModule,
    MatComponentsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
