import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface ToastMessage {
  text: string;
  type: 'success' | 'error' | 'warning';
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  toastSubject = new Subject<ToastMessage>();
  toastState = this.toastSubject.asObservable();

  show(text: string, type: 'success' | 'error' | 'warning' = 'error') {
    this.toastSubject.next({ text, type });
  }
}
