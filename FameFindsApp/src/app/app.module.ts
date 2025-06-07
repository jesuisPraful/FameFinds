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

@NgModule({
  declarations: [
    AppComponent,
    ViewProductsComponent,
    RegistrationPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [ProductService, RegisterService],
  bootstrap: [AppComponent]
})
export class AppModule { }
