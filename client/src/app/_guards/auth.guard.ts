import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {

  const accountService: AccountService = inject(AccountService);
  const toastr: ToastrService = inject(ToastrService);

  if(accountService.currentUser()){
    return true;
  } else {
    toastr.error('Please log in to your account.');
    return false;
  }
};
