import { Component, Input } from '@angular/core';
import { NgModel } from '@angular/forms';

@Component({
  selector: 'app-input-validation-message',
  standalone: true,
  imports: [],
  templateUrl: './input-validation-message.component.html',
  styleUrl: './input-validation-message.component.css'
})
export class InputValidationMessageComponent {
  @Input({required: true}) ngModel!: NgModel;
  @Input({required: true}) errorMessage!: string;
}
