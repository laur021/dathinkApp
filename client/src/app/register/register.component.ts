import { Component, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  
  //PARENT TO CHILD
  // OLD WAY: 
  // @Input() userFromHomeComponent: any; 
  // NEW WAY: 
  userFromHomeComponent = input.required<any>(); 

  
  // CHILD TO PARENT
  // OLD WAY:
  // @Output() cancelRegister = new EventEmitter();
  // NEW WAY:
  cancelRegister = output<boolean>();

  model: any = {};

  register() {
    console.log(this.model);
  }

  cancel(){
    this.cancelRegister.emit(false);
  }

}
