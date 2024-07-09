import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../login/services/auth.service';
import { User } from '../login/models/user.model';

@Component({
  selector: 'app-menu-header',
  standalone: true,
  imports: [RouterLink,RouterLinkActive],
  templateUrl: './menu-header.component.html',
  styleUrl: './menu-header.component.css'
})
export class MenuHeaderComponent implements OnInit {
  user?: User;
  isAdmin?: boolean;

  constructor(private authService: AuthService, private router: Router) {

  }

  ngOnInit(): void {
    this.authService.user()
    .subscribe({
      next: (response) => {
        this.user = response;
        this.isAdmin = response?.roles.includes('Writer');
      }
    });

    this.user = this.authService.getUser();
    this.isAdmin = this.authService.isAdmin();
  }

  onLogout(): void {
    if(confirm(`Are you sure to log out ${this.user?.email}?`)) {
      this.authService.logout();
      this.router.navigateByUrl('/');
    }
  }

}
