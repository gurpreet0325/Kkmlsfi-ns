import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-homecell-popup',
  standalone: true,
  imports: [],
  templateUrl: './homecell-popup.component.html',
  styleUrl: './homecell-popup.component.css'
})
export class HomecellPopupComponent {
  @Input({required: true}) mode!: string;

  onCancel() {
    //this.isPopupOpen = false;
  }

  onFormSubmit() {

  }
}
