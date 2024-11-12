import { Component, inject } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { AccountService } from "../_services/account.service";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { Router, RouterLink, RouterLinkActive } from "@angular/router";

@Component({
  selector: "app-nav",
  standalone: true,
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive],
  templateUrl: "./nav.component.html",
  styleUrl: "./nav.component.css",
})
export class NavComponent {
  
  accountService = inject(AccountService); // Inject AccountService
  private router = inject(Router); //inject the router for navigation route
  model: any = {};

  login() {
    this.accountService.login(this.model).subscribe({
      next: _ => this.router.navigateByUrl('/members'),
      error: (error: any) => console.log(error),
    });
  }

  logout() {
    this.accountService.logout()
    this.router.navigateByUrl('/')
  }
}
