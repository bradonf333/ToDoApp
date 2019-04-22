import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { config } from 'src/app/app.config';
import { User } from 'src/app/Models/User';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  title = 'To-Do List App';
  currentUser: User;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem(config.userKey));
    console.log('CurrentUser: ', this.currentUser);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
