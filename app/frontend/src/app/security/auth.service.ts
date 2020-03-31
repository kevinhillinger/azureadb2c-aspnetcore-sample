import { MsalService, BroadcastService } from "@azure/msal-angular";
import { Injectable, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs";
import { UserFlow } from "./userFlow.enum";
import { AuthError, AuthResponse, AuthenticationParameters } from "msal";

interface MSALError {
  error: string;
  errorDesc: string;
  scopes: string;
}

@Injectable()
export class AuthService implements OnDestroy {
  private isAuthenticated: boolean;
  private readonly authorityBaseUrl = "https://idhack007.b2clogin.com/tfp/idhack007.onmicrosoft.com/";
  private readonly loginFailureTopic = "msal:loginFailure";
  private readonly loginSuccessTopic = "msal:loginSuccess";
  private readonly forgotPasswordCode = "AADB2C90118:";

  private readonly subscriptions = new Map();
  private readonly scopes = ["https://idhack007.onmicrosoft.com/webapp-sample-api/weatherforecast.read"];

  constructor(private msalService: MsalService, private broadcastService: BroadcastService) {
  }

  getIsAuthenticated() {
    return this.isAuthenticated;
  }

  register() {

    this.subscriptions.set(this.loginFailureTopic, this.broadcastService.subscribe(this.loginFailureTopic, this.loginFailureHandler.bind(this)));
    this.subscriptions.set(this.loginSuccessTopic, this.broadcastService.subscribe(this.loginSuccessTopic, this.loginSuccessHandler.bind(this)));

    let scopes = this.scopes;

    this.msalService.handleRedirectCallback((redirectError: AuthError, redirectResponse: AuthResponse) => {
      if (redirectError) {
        console.error("Redirect error: ", redirectError);
        return;
      }
      console.log("Redirect success: ", redirectResponse);
      this.msalService.acquireTokenSilent({
        scopes: scopes
      })
    });

  }

  signIn() {
    let request: AuthenticationParameters = {
      scopes: this.scopes
    };
    this.msalService.loginRedirect(request);
  }
  
  signOut() {
    this.msalService.logout();
  }

  resetPassword() {
    this.handleForgotPassword();
  }

  private loginSuccessHandler(payload) {
    this.isAuthenticated = true;

    console.log(payload);
    console.log("login success.");
    
  }

  private loginFailureHandler(payload: MSALError) {
    this.isAuthenticated = false;

    console.log(payload);
    console.log("login failure.");
    
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

    this.disposeSubscription(this.loginFailureTopic);
    this.disposeSubscription(this.loginSuccessTopic);
  }

  private disposeSubscription(topic: string) {
    let subscription:Subscription = this.subscriptions.get(topic);

    if (subscription) {
      subscription.unsubscribe();
    }
  }

  ngOnDestroy(): void {
    this.disposeSubscriptions();
  }


}