import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent {
  public profile: Profile;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Profile>(baseUrl + 'profile').subscribe(result => {
      this.profile = result;
    }, error => console.error(error));
  }
}

interface Profile {
  claims: Map<string, string>;
}
