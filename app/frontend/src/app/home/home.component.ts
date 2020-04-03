import { Component, OnInit } from '@angular/core';
import { AuthService } from '../security/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  displayName: string;
  welcomeMessage: string;
  isAuthenticated: boolean;

  constructor(private authService: AuthService) {
  }
  
  resetPassword() {
    this.authService.resetPassword();
  }

  signIn() {
    this.authService.signIn();
  }
  
  signInFederated() {
    this.authService.signInFederated();
  }
  
  signOut() {
    this.authService.signOut();
  }

  ngOnInit(): void {
   this.isAuthenticated = this.authService.getIsAuthenticated();
  }
}
