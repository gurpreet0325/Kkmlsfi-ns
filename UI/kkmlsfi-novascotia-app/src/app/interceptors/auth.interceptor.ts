import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../login/services/auth.service';


export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const shouldInterceptRequest = req.urlWithParams.indexOf('addAuth=true',0) > -1? true: false;

  if (shouldInterceptRequest) {
    const authToken = inject(AuthService).getBearerToken();
    const authRequest = req.clone({
      headers: req.headers.set('Authorization', authToken)
    });

    return next(authRequest);
  }
  
  return next(req);
};
