import { HttpInterceptorFn } from "@angular/common/http";
import { AccountService } from "../_services/account.service";
import { inject } from "@angular/core";

//req is a immutable object, so it needs to clone and add header authentication
export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountService);

  //clone the req then add token
  if (accountService.currentUser()) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accountService.currentUser()?.token}`,
      },
    });
  }

  return next(req);
};
