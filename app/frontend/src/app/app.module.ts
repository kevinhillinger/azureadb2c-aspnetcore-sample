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
import { environment } from 'src/environments/environment';

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
        clientId: environment.auth.clientId,
        navigateToLoginRequestUrl : false,
        redirectUri: environment.auth.redirectUri,
        postLogoutRedirectUri: environment.auth.postLogoutRedirectUri,
        validateAuthority: false, //must be false to support B2C
        authority: environment.auth.authority
      },
      cache: {
        cacheLocation : "sessionStorage",
        storeAuthStateInCookie: true
      }
    },
    {
      popUp: false, 
      consentScopes: environment.consentScopes
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
