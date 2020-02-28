import { Component, OnInit } from '@angular/core';
import { MsalService} from "@azure/msal-angular";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  displayName: string;
  welcomeMessage: string;
  
  constructor(private authService: MsalService) {
  }

  ngOnInit(): void {
    let scopes = ["https://idhack007.onmicrosoft.com/webapp-sample-api/weatherforecast.read"];

    this.authService.acquireTokenSilent(scopes, this.authService.authority).then((token) => {
      console.log("acquired token silently");

       let user = this.authService.getUser();

        if (user.name) {
          this.displayName = this.authService.getUser().name;
          this.welcomeMessage = "Hello, " + this.displayName + "!";
        }
      }, (error) => {
          console.log('problem with getting token silenty');
          console.log(error);
    });
  }
}
