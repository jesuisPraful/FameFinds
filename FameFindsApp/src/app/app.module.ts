import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { ProductService } from './services/product.service';
import { RegisterService } from './services/register.service';
import { CustomerService } from './services/customer.service';
import { VendorService } from './services/vendor.service';
import { ShopService } from './services/shop.service';
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
import { UpdateShopComponent } from './components/update-shop/update-shop.component';
import { IsOpenComponent } from './components/is-open/is-open.component';
import { RemoveShopComponent } from './components/remove-shop/remove-shop.component';
import { AddCityComponent } from './components/add-city/add-city.component';
import { RemoveCityComponent } from './components/remove-city/remove-city.component';
import { AddShopsComponent } from './components/add-shops/add-shops.component';
import { ForgotpasswordService } from './services/forgotpassword.service';

@NgModule({
  declarations: [
    AppComponent,
    ViewProductsComponent,
    RegistrationPageComponent,
    LoginComponent,
    LandingComponent,
    LoginVendorComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    OtpVerificationComponent,
    ViewShopsComponent,
    ViewCitiesComponent,
    UpdateShopComponent,
    IsOpenComponent,
    RemoveShopComponent,
    AddCityComponent,
    RemoveCityComponent,
    AddShopsComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    ProductService,
    RegisterService,
    CustomerService,
    VendorService,
    ShopService,
    ForgotpasswordService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
