import { Component, inject } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { AccountService } from "../_services/account.service";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";

@Component({
  selector: "app-nav",
  standalone: true,
  imports: [FormsModule, BsDropdownModule],
  templateUrl: "./nav.component.html",
  styleUrl: "./nav.component.css",
})
export class NavComponent {
  // Inject AccountService
  private accountService = inject(AccountService);
  loggedIn = false;
  model: any = {};

  // Define login as a standard method
  login() {
    this.accountService.login(this.model).subscribe({
      next: (response: any) => {
        console.log(response);
        this.loggedIn = true;
      },
      error: (error: any) => console.log(error),
    });
  }

  logout() {
    this.loggedIn = false;
  }
}
