import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './components/landing/landing.component';
import { RegistrationPageComponent } from './components/registration-page/registration-page.component';
import { RegistrationPage2Component } from './components/registration-page2/registration-page2.component';

const routes: Routes = [
  //{ path: 'login/user', component: LandingComponent},
  //{ path: 'login/vendor', component: LandingComponent },
  //{ path: 'register/user', component: RegistrationPageComponent },
  //{ path: 'register/vendor', component: RegistrationPage2Component },
  //{ path: '', redirectTo: '/home', pathMatch: 'full' },
  //{ path: '**', redirectTo: '/home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


