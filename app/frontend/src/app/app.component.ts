import { Component, OnInit } from '@angular/core';
import { AuthService } from './security/auth.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(private authService: AuthService) {
  }

  ngOnInit(): void {
    this.authService.register();
  }
}
