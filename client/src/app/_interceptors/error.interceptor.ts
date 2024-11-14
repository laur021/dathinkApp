import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { NavigationExtras, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { catchError } from "rxjs";

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  // Inject required services
  const router = inject(Router); // Router service for navigation
  const toastr = inject(ToastrService); // Toastr service for showing notifications

  return next(req).pipe(
    catchError((error) => {
      if (error) {
        switch (error.status) {
          case 400:
            if (error.error.errors) {
              // If validation errors are present, collect and throw them as a flat array
              const modalStateErrors = [];
              for (const key in error.error.errors) {
                if (error.error.errors[key]) {
                  modalStateErrors.push(error.error.errors[key]);
                }
              }
              throw modalStateErrors.flat();
            } else {
              // Otherwise, display a toast notification for the error
              toastr.error(error.error, error.status);
            }
            break;
          case 401:
            // Display "Unauthorised" toast message for 401 status
            toastr.error("Unauthorised", error.status);
            break;
          case 404:
            // Redirect to 'not-found' page for 404 status
            router.navigateByUrl("not-found");
            break;
          case 500:
            // Redirect to 'server-error' page for 500 status, passing error details in navigation state
            const navigationExtras: NavigationExtras = {
              state: { error: error.error },
            };
            router.navigateByUrl("server-error", navigationExtras);
            break;
          default:
            // Show a generic error message for any other status
            toastr.error("Something unexpected went wrong");
            break;
        }
      }
      throw error; // Rethrow the error for any further handling if necessary
    })
  );
};

/*
Explanation of the errorInterceptor process:

1. This interceptor is designed to handle HTTP errors consistently across the application.
2. It injects required services:
   - `Router` for navigation to error pages.
   - `ToastrService` for displaying toast notifications to the user.
3. The interceptor captures HTTP responses and checks for errors using `catchError`.
4. For specific error statuses:
   - **400 (Bad Request)**: If there are validation errors, it collects them and throws as a flat array.
     Otherwise, it shows a toast notification with the error message and status.
   - **401 (Unauthorized)**: Shows an "Unauthorised" toast notification.
   - **404 (Not Found)**: Redirects the user to a 'not-found' page.
   - **500 (Internal Server Error)**: Redirects to a 'server-error' page, passing error details.
   - **Default Case**: For any other errors, shows a generic "unexpected error" toast message.
5. By handling errors this way, the application provides consistent feedback and navigation for users.
*/
