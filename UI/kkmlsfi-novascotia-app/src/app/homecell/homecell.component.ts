import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Observable  } from 'rxjs';
import { Homecell } from './models/homecell.model';
import { HomecellService } from './services/homecell.service';
import { AuthService } from '../login/services/auth.service';
import { HomecellPopupComponent } from "./homecell-popup/homecell-popup.component";

@Component({
  selector: 'app-homecell',
  standalone: true,
  imports: [FormsModule, CommonModule, HomecellPopupComponent],
  templateUrl: './homecell.component.html',
  styleUrl: './homecell.component.css'
})
export class HomecellComponent implements OnInit, OnDestroy {
  isAdmin: boolean = false;
  homecells$?: Observable<Homecell[]>;
  isPopupOpen: boolean = false;
  mode!: string;
  searchFilter: string = '';

  constructor(private homecellService: HomecellService, private authService: AuthService) {}

  ngOnDestroy(): void {
    
  }
  ngOnInit(): void {
    this.isAdmin = this.authService.isAdmin();

    this.loadHomecells();
  }

  onSearch(searchFilter: string) {

  }

  onDeleteHomecell(homecell: Homecell) {

  }

  private loadHomecells() {
    this.homecells$ = this.homecellService.getAllHomecells();
  }

  onAddHomecell() {
    this.mode = 'Add';
    this.isPopupOpen = true;
  }

  onUpdateHomecell() {
    this.mode = 'Update';
    this.isPopupOpen = true;
  }

  
}
