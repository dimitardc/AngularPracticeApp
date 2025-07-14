import { CanActivateFn } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.currentUser) {
    router.navigate(['/register-passenger', { requestedUrl: state.url }]);
  }

  return true; // Allow navigation
};
