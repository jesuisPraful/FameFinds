import { AfterViewInit, Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements AfterViewInit {
  ngAfterViewInit(): void {
    const dropdowns = document.querySelectorAll('nav .dropdown');

    dropdowns.forEach(dropdown => {
      const trigger = dropdown.querySelector('a');
      const menu = dropdown.querySelector('.dropdown-menu');
      const links = Array.from(menu?.querySelectorAll('a') || []);

      trigger?.addEventListener('keydown', (e) => {
        if (e.key === 'ArrowDown' || e.key === 'Enter' || e.key === ' ') {
          e.preventDefault();
          (links[0] as HTMLElement)?.focus();
        }
      });

      links.forEach((link, index) => {
        link.addEventListener('keydown', (e) => {
          if (e.key === 'ArrowDown') {
            e.preventDefault();
            const next = links[(index + 1) % links.length] as HTMLElement;
            next.focus();
          } else if (e.key === 'ArrowUp') {
            e.preventDefault();
            const prev = links[(index - 1 + links.length) % links.length] as HTMLElement;
            prev.focus();
          } else if (e.key === 'Escape') {
            e.preventDefault();
            (trigger as HTMLElement)?.focus();
            (menu as HTMLElement).style.visibility = 'hidden';
            (menu as HTMLElement).style.opacity = '0';
            (menu as HTMLElement).style.transform = 'translateY(10px)';
          }
        });
      });

      dropdown.addEventListener('mouseleave', () => {
        (menu as HTMLElement).style.visibility = 'hidden';
        (menu as HTMLElement).style.opacity = '0';
        (menu as HTMLElement).style.transform = 'translateY(10px)';
      });

      dropdown.addEventListener('mouseenter', () => {
        (menu as HTMLElement).style.visibility = 'visible';
        (menu as HTMLElement).style.opacity = '1';
        (menu as HTMLElement).style.transform = 'translateY(0)';
      });
    });
  }
}
//import { NgModule } from '@angular/core';
//import { RouterModule, Routes } from '@angular/router';
//import { LandingComponent } from './pages/landing/landing.component';

//const routes: Routes = [
//  { path: '', component: LandingComponent },
//  { path: 'login', loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule) },
//  { path: 'register', loadChildren: () => import('./pages/register/register.module').then(m => m.RegisterModule) }
//];

//@NgModule({
//  imports: [RouterModule.forRoot(routes)],
//  exports: [RouterModule]
//})
//export class AppRoutingModule { }
