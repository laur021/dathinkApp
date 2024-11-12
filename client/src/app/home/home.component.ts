import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  http = inject(HttpClient);
  accountService = inject(AccountService);
  registerMode = false;
  users: any;

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

  getUsers() {
    this.http.get("https://localhost:5001/api/users").subscribe({
      next: (response) => (this.users = response),
      error: (error) => console.log(error),
      complete: () => console.log("Request has completed"),
    });
  }
}
