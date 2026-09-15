import { Component, OnInit } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { ToastService, ToastMessage } from './services/toast.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, CommonModule],
  template: `
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary mb-4">
      <div class="container">
        <a class="navbar-brand" routerLink="/">Employee Registration App</a>
        <div class="navbar-nav">
          <a class="nav-link" routerLink="/employees">Employee List</a>
          <a class="nav-link" routerLink="/employee/new">New Employee</a>
        </div>
      </div>
    </nav>
    <div class="container">
      <router-outlet></router-outlet>
    </div>

    <div *ngIf="toast" class="toast-container position-fixed top-0 end-0 p-3" style="z-index: 1050;">
      <div class="toast show align-items-center text-white border-0" [ngClass]="{'bg-danger': toast.type === 'error', 'bg-success': toast.type === 'success', 'bg-warning': toast.type === 'warning'}" role="alert">
        <div class="d-flex">
          <div class="toast-body">
            {{ toast.text }}
          </div>
          <button type="button" class="btn-close btn-close-white me-2 m-auto" (click)="toast = null"></button>
        </div>
      </div>
    </div>
  `
})
export class AppComponent implements OnInit {
  toast: ToastMessage | null = null;

  constructor(private toastService: ToastService) {}

  ngOnInit() {
    this.toastService.toastState.subscribe(msg => {
      this.toast = msg;
      setTimeout(() => this.toast = null, 5000);
    });
  }
}
