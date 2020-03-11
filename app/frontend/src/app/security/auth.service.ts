import { MsalService, BroadcastService } from "@azure/msal-angular";
import { Injectable, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs";
import { UserFlow } from "./userFlow.enum";

interface MSALError {
  error: string;
  errorDesc: string;
  scopes: string;
}

@Injectable()
export class AuthService implements OnDestroy {
  
  private readonly authorityBaseUrl = "https://idhack007.b2clogin.com/tfp/idhack007.onmicrosoft.com/";
  private readonly loginFailureTopic = "msal:loginFailure";
  private readonly forgotPasswordCode = "AADB2C90118:";

  private readonly subscriptions = new Map();
  private readonly scopes = ["https://idhack007.onmicrosoft.com/webapp-sample-api/weatherforecast.read"];

  constructor(private msalService: MsalService, private broadcastService: BroadcastService) {
    this.broadcastService.subscribe(this.loginFailureTopic, this.loginFailureHandler.bind(this));
  }

  isAuthenticated() {
    return this.msalService.getUser() != undefined;
  }

  signIn() {
    this.msalService.loginRedirect();
  }
  
  signOut() {
    this.msalService.logout();
  }

  resetPassword() {
    this.handleForgotPassword();
  }

  private loginFailureHandler(payload: MSALError) {

    console.log(this.msalService.user);
    
    // check if we're getting a redirect back from B2C to handle the password reset. If we are, handle it.
    if (this.isForgotPasswordFlow(payload)) {
      this.handleForgotPassword();
    }
  }

  private isForgotPasswordFlow(payload: MSALError) {
    if (payload) {
      return payload.error == "access_denied" && payload.errorDesc.startsWith(this.forgotPasswordCode);
    }
    return false;
  }
  /**
   * Handles the redirect back to B2C for resetting the password
   */
  private handleForgotPassword() {
    this.msalService.authority = this.authorityBaseUrl + UserFlow.PasswordReset;
    this.msalService.loginRedirect();
  }

  private disposeSubscriptions() {
    this.broadcastService.getMSALSubject().next(1);

    let loginFailureSubscription:Subscription = this.subscriptions.get(this.loginFailureTopic);

    if (loginFailureSubscription) {
      loginFailureSubscription.unsubscribe();
    }

  }

  ngOnDestroy(): void {
    this.disposeSubscriptions();
  }


}