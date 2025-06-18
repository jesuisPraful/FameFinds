import { AfterViewInit, Component, ElementRef, Renderer2 } from '@angular/core';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements AfterViewInit {
  constructor(private elRef: ElementRef, private renderer: Renderer2) { }

  ngAfterViewInit(): void {
    const dropdowns = this.elRef.nativeElement.querySelectorAll('nav .dropdown');

    dropdowns.forEach((dropdown: HTMLElement) => {
      const trigger = dropdown.querySelector('a');
      const menu = dropdown.querySelector('.dropdown-menu');
      const links = Array.from(menu?.querySelectorAll('a') || []);

      if (!trigger || !menu) return;

      // Hover effects
      this.renderer.listen(dropdown, 'mouseenter', () => {
        this.renderer.setStyle(menu, 'visibility', 'visible');
        this.renderer.setStyle(menu, 'opacity', '1');
        this.renderer.setStyle(menu, 'transform', 'translateY(0)');
      });

      this.renderer.listen(dropdown, 'mouseleave', () => {
        this.renderer.setStyle(menu, 'visibility', 'hidden');
        this.renderer.setStyle(menu, 'opacity', '0');
        this.renderer.setStyle(menu, 'transform', 'translateY(10px)');
      });

      // Trigger keyboard navigation
      this.renderer.listen(trigger, 'keydown', (e: KeyboardEvent) => {
        if (['ArrowDown', 'Enter', ' '].includes(e.key)) {
          e.preventDefault();
          (links[0] as HTMLElement)?.focus();
        }
      });

      // Menu item navigation
      links.forEach((link, index) => {
        this.renderer.listen(link, 'keydown', (e: KeyboardEvent) => {
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
            this.renderer.setStyle(menu, 'visibility', 'hidden');
            this.renderer.setStyle(menu, 'opacity', '0');
            this.renderer.setStyle(menu, 'transform', 'translateY(10px)');
          }
        });
      });
    });
  }
}
