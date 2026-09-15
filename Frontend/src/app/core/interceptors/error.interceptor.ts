import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { ToastService } from '../../services/toast.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastService = inject(ToastService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let message = 'An unexpected error occurred';

      if (error.status === 0) {
        message = 'Cannot connect to the server. Please make sure the API is running.';
      } else if (error.status === 404) {
        message = 'Resource not found.';
      } else if (error.status === 500) {
        message = 'Internal server error. Please try again later.';
      } else if (error.error?.message) {
        message = error.error.message;
      }

      toastService.show(message, 'error');
      return throwError(() => error);
    })
  );
};
