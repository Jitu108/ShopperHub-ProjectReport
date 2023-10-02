import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrandFormComponent } from './components/brands/brand-form/brand-form.component';
import { BrandListComponent } from './components/brands/brand-list/brand-list.component';
import { CatalogTypeFormComponent } from './components/catalog-types/catalog-type-form/catalog-type-form.component';
import { CatalogTypeListComponent } from './components/catalog-types/catalog-type-list/catalog-type-list.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ProductFormComponent } from './components/products/product-form/product-form.component';
import { ProductListComponent } from './components/products/product-list/product-list.component';
import { UserLoginComponent } from './components/users/user-login/user-login.component';
import { AuthGuard } from './services/auth.guard';

const routes: Routes = [
  { path: 'brands', component: BrandListComponent, canActivate: [AuthGuard] },
  { path: 'brands/detail', component: BrandFormComponent, canActivate: [AuthGuard] },
  { path: 'types', component: CatalogTypeListComponent, canActivate: [AuthGuard] },
  { path: 'types/detail', component: CatalogTypeFormComponent, canActivate: [AuthGuard] },
  { path: 'catalogs', component: ProductListComponent, canActivate: [AuthGuard] },
  { path: 'catalogs/detail', component: ProductFormComponent, canActivate: [AuthGuard] },
  { path: 'products', component: ProductListComponent, canActivate: [AuthGuard] },
  { path: 'products/detail', component: ProductFormComponent, canActivate: [AuthGuard] },
  { path: 'login', component: UserLoginComponent },
  { path: 'not-found', component: PageNotFoundComponent },
  { path: '**', redirectTo: "not-found" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
