import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  console.log('token ',authService.getTokenFromLocalStorage())
  if (authService.getTokenFromLocalStorage()) {
    return true;
  } else {
    router.navigate(['/login']);
    return false;
  }
};