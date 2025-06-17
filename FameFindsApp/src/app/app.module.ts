import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { GoogleMapsModule } from '@angular/google-maps';

import { AppComponent } from './app.component';
import { ProductService } from './services/product.service';
import { RegisterService } from './services/register.service';
import { CustomerService } from './services/customer.service';
import { VendorService } from './services/vendor.service';
import { ShopService } from './services/shop.service';
 import { ForgotpasswordvendorService } from './services/forgotpasswordvendor.service';
 import { ForgotpasswordService } from './services/forgotpassword.service';

import { ViewProductsComponent } from './components/view-products/view-products.component';
import { RegistrationPageComponent } from './components/registration-page/registration-page.component';
import { LoginComponent } from './components/login/login.component';
import { LandingComponent } from './components/landing/landing.component';
import { LoginVendorComponent } from './components/login-vendor/login-vendor.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { OtpVerificationComponent } from './components/otp-verification/otp-verification.component';
import { ViewShopsComponent } from './components/view-shops/view-shops.component';
import { ViewCitiesComponent } from './components/view-cities/view-cities.component';
import { ViewProductsComponent } from './components/view-products/view-products.component';
import { UpdateShopComponent } from './components/update-shop/update-shop.component';
import { IsOpenComponent } from './components/is-open/is-open.component';
import { RemoveShopComponent } from './components/remove-shop/remove-shop.component';
import { AddCityComponent } from './components/add-city/add-city.component';
import { RemoveCityComponent } from './components/remove-city/remove-city.component';
import { AddShopsComponent } from './components/add-shops/add-shops.component';
import { RegistrationPage2Component } from './components/registration-page2/registration-page2.component';
import { Register2Service } from './services/register2.service';
import { ForgotPasswordVendorComponent } from './components/forgot-password-vendor/forgot-password-vendor.component';
import { ShopDetailsComponent } from './components/shop-details/shop-details.component';
import { RatingComponent } from './components/rating/rating.component';
import { CommonModule } from '@angular/common';
import { ForgotpasswordvendorService } from './services/forgotpasswordvendor.service';
import { ForgotpasswordService } from './services/forgotpassword.service';
import { GoogleMapsModule } from '@angular/google-maps';
import { VendorCommonlayoutComponent } from './components/vendor-commonlayout/vendor-commonlayout.component';

import { RegistrationPage2Component } from './components/registration-page2/registration-page2.component';
import { Register2Service } from './services/register2.service';

 

import { VendorDashboardComponent } from './components/vendor-dashboard/vendor-dashboard.component';
import { UpdateShopContactnumberComponent } from './components/updateshop-contactnumber/updateshop-contactnumber.component';
import { UpdateshopEmailComponent } from './components/updateshop-email/updateshop-email.component';
import { UpdateshopNameComponent } from './components/updateshop-name/updateshop-name.component';


@NgModule({
  declarations: [
    AppComponent,
    ViewProductsComponent,
    RegistrationPageComponent,
    RegistrationPage2Component,
    LoginComponent,
    LandingComponent,
    LoginVendorComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    OtpVerificationComponent,
    ViewShopsComponent,
    ViewCitiesComponent,
    ViewProductsComponent,
    UpdateShopComponent,
    IsOpenComponent,
    RemoveShopComponent,
    AddCityComponent,
    RemoveCityComponent,
    AddShopsComponent,
    ForgotPasswordVendorComponent,
    ShopDetailsComponent,
    ForgotPasswordVendorComponent,
    RatingComponent,
    VendorDashboardComponent,
    UpdateShopContactnumberComponent,
    UpdateshopEmailComponent,
    UpdateshopNameComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    GoogleMapsModule,
  //  BrowserAnimationsModule
  ],
  providers: [
    ProductService,
    RegisterService,
    Register2Service,
    CustomerService,
    VendorService,
    ShopService,
    ForgotpasswordvendorService,
    ForgotpasswordService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
