import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http'; 
import { AppComponent } from './app.component';
import { ProductService } from './services/product.service';
import { ViewProductsComponent } from './components/view-products/view-products.component';
import { FormsModule } from '@angular/forms';
import { RegisterService } from './services/register.service';
import { RegistrationPageComponent } from './components/registration-page/registration-page.component';
import { LoginComponent } from './components/login/login.component';
import { CustomerService } from './services/customer.service';
import { LandingComponent } from './components/landing/landing.component';
import { VendorService } from './services/vendor.service';
import { LoginVendorComponent } from './components/login-vendor/login-vendor.component';
import { RegistrationPage2Component } from './components/registration-page2/registration-page2.component';

@NgModule({
  declarations: [
    AppComponent,
    ViewProductsComponent,
    RegistrationPageComponent,
    LoginComponent,
    RegistrationPage2Component,
    LandingComponent,
    LoginVendorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [ProductService, RegisterService, CustomerService, VendorService],
  bootstrap: [AppComponent]
})
export class AppModule { }
