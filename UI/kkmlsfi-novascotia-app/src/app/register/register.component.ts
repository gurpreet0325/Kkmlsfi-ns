import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterRequest } from './models/register-request.models';
import { RegisterService } from './services/register.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  model: RegisterRequest;
  confirmPassword: string = '';

  constructor(private registerService: RegisterService, private router: Router) {
    this.model = {
      email: '',
      password: '',
      isAdmin: false
    };
  }

  onFormSubmit(): void {
    if (this.model.password === this.confirmPassword) {
      this.registerService.register(this.model).
      subscribe({
        next: (response) => {
          this.router.navigateByUrl('/login');
        }
      });
    }
  }
}
