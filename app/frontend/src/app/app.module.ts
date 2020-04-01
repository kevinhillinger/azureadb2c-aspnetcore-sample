import { MsalModule, MsalGuard, MsalInterceptor } from '@azure/msal-angular'
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ProfileComponent } from './profile/profile.component';
import { AuthService } from './security/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ProfileComponent
  ],
  imports: [
    MsalModule.forRoot({ 
      auth: {
        clientId: '1f16026e-109a-4f94-8985-83fd0fcd10a6',
        navigateToLoginRequestUrl : false,
        redirectUri: "https://localhost:4201/",
        postLogoutRedirectUri: "https://localhost:4201/",
        validateAuthority: false, //must be false to support B2C
        authority: "https://idhack007.b2clogin.com/tfp/idhack007.onmicrosoft.com/B2C_1A_signup_signin/" //default authority is signin signup user flow
      },
      cache: {
        cacheLocation : "sessionStorage",
        storeAuthStateInCookie: true
      }
    },
    {
      popUp: false, 
      consentScopes: ["https://idhack007.onmicrosoft.com/webapp-sample-api/weatherforecast.read"]
    }),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent, canActivate : [MsalGuard] },
      { path: 'fetch-data', component: FetchDataComponent, canActivate : [MsalGuard] },
      { path: 'profile', component: ProfileComponent, canActivate : [MsalGuard] },
    ])
  ],
  providers: [
    AuthService,
    {provide: HTTP_INTERCEPTORS, useClass: MsalInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
