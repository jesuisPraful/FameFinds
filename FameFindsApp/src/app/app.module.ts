import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentComponent } from './component/component.component';
import { ViewShopsComponent } from './components/view-shops/ViewShopsComponent';
import { ServicesComponent } from './services/services.component';

@NgModule({
  declarations: [
    AppComponent,
    ComponentComponent,
    ViewShopsComponent,
    ServicesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
