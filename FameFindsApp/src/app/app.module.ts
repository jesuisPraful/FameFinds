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
import { ViewShopsComponent } from './components/view-shops/view-shops.component';
import { ViewCitiesComponent } from './components/view-cities/view-cities.component';
import { AddShopsComponent } from './components/add-shops/add-shops.component'; // ✅ adjust path if different


@NgModule({
  declarations: [
    AppComponent,
    ViewProductsComponent,
    RegistrationPageComponent,
    LoginComponent,
    ViewShopsComponent,
    ViewCitiesComponent,
    AddShopsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [ProductService, RegisterService, CustomerService],
  bootstrap: [AppComponent]
})
export class AppModule { }
