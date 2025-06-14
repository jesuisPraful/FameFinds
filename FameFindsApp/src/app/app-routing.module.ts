import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './components/landing/landing.component';
import { RegistrationPageComponent } from './components/registration-page/registration-page.component';
import { RegistrationPage2Component } from './components/registration-page2/registration-page2.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { OtpVerificationComponent } from './components/otp-verification/otp-verification.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { LoginComponent } from './components/login/login.component';
import { LoginVendorComponent } from './components/login-vendor/login-vendor.component';

const routes: Routes = [
  { path: '', component: LandingComponent }, 
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'verify-otp', component: OtpVerificationComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationPageComponent },
  { path: 'vendorlogin', component: LoginVendorComponent },
  { path: 'vendorregister', component: RegistrationPage2Component },
  { path: '**', component: LandingComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


