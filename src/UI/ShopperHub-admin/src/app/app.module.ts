import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TopHeaderComponent } from './components/top-header/top-header.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrandListComponent } from './components/brands/brand-list/brand-list.component';
import { BrandFormComponent } from './components/brands/brand-form/brand-form.component';
import { CatalogTypeListComponent } from './components/catalog-types/catalog-type-list/catalog-type-list.component';
import { CatalogTypeFormComponent } from './components/catalog-types/catalog-type-form/catalog-type-form.component';
import { ProductListComponent } from './components/products/product-list/product-list.component';
import { ProductFormComponent } from './components/products/product-form/product-form.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UserLoginComponent } from './components/users/user-login/user-login.component';
import { MatCommonModule } from '@angular/material/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatComponentsModule } from './modules/mat-components/mat-components.module'
import { HttpClientModule } from '@angular/common/http';
import { DropDownListComponent } from './components/shared/drop-down-list/drop-down-list.component';

@NgModule({
  declarations: [
    AppComponent,
    TopHeaderComponent,
    BrandListComponent,
    BrandFormComponent,
    CatalogTypeListComponent,
    CatalogTypeFormComponent,
    ProductListComponent,
    ProductFormComponent,
    PageNotFoundComponent,
    UserLoginComponent,
    DropDownListComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    BrowserAnimationsModule,
    MatCommonModule,
    MatComponentsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
