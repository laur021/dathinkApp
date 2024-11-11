import { Component, input } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  
  // @Input() userFromHomeComponent: any; // OLD WAY: This property will hold the data from the parent
  userFromHomeComponent = input.required<any>(); // NEW WAY: This property will hold the data from the parent
  model: any = {};

  register() {
    console.log(this.model);
  }

  cancel() {
    console.log('cancelled');
  }
}
