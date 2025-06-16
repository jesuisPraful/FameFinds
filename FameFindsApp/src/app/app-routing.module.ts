import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './components/landing/landing.component';
import { RegistrationPageComponent } from './components/registration-page/registration-page.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { OtpVerificationComponent } from './components/otp-verification/otp-verification.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { LoginComponent } from './components/login/login.component';
import { LoginVendorComponent } from './components/login-vendor/login-vendor.component';
import { ViewShopsComponent } from './components/view-shops/view-shops.component';
import { ViewCitiesComponent } from './components/view-cities/view-cities.component';
import { UpdateShopComponent } from './components/update-shop/update-shop.component';
import { IsOpenComponent } from './components/is-open/is-open.component'; // Add this line
import { RemoveShopComponent } from './components/remove-shop/remove-shop.component';
import { AddCityComponent } from './components/add-city/add-city.component';
import { RemoveCityComponent } from './components/remove-city/remove-city.component';
import { AddShopsComponent } from './components/add-shops/add-shops.component';
import { ViewProductsComponent } from './components/view-products/view-products.component';
import { RegistrationPage2Component } from './components/registration-page2/registration-page2.component';

const routes: Routes = [
  { path: '', component: LandingComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'verify-otp', component: OtpVerificationComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationPageComponent },
  { path: 'vendorlogin', component: LoginVendorComponent },
  { path:'vendorregister',component:RegistrationPage2Component},
  { path: 'view-shops', component: ViewShopsComponent },
  { path: 'view-cities', component: ViewCitiesComponent },
  { path: 'update-shop', component: UpdateShopComponent }, // optional version without route param
  { path: 'update-shop/:id', component: UpdateShopComponent }, // version with route param
  { path: 'is-open', component: IsOpenComponent }, // new route for is-open component
  { path: 'remove-shop', component: RemoveShopComponent },
  { path: 'add-city', component: AddCityComponent },
  { path: 'remove-city', component: RemoveCityComponent },
  { path: 'add-shops', component: AddShopsComponent },
  { path: 'view-products', component: ViewProductsComponent },
  { path: '**', component: LandingComponent }, // wildcard should come last

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
