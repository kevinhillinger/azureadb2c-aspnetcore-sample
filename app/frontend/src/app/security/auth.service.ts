import { MsalService, BroadcastService } from "@azure/msal-angular";
import { Injectable, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs";
import { UserFlow } from "./userFlow.enum";
import { AuthError, AuthResponse, AuthenticationParameters } from "msal";
import { environment } from "../../environments/environment"

interface MSALError {
  error: string;
  errorDesc: string;
  scopes: string;
}

enum MsalTopic {
  loginFailure = "msal:loginFailure",
  loginSuccess = "msal:loginSuccess",
  acquireTokenSuccess = "msal:acquireTokenSuccess",
  acquirTokenFailure = "msal:acquireTokenFailure"
}

@Injectable()
export class AuthService implements OnDestroy {
  private isAuthenticated: boolean;
  private readonly authorityBaseUrl = "https://idhack007.b2clogin.com/tfp/idhack007.onmicrosoft.com/";
  private readonly forgotPasswordCode = "AADB2C90118:";

  private readonly subscriptions = new Map();
  private readonly scopes = ["https://idhack007.onmicrosoft.com/webapp-sample-api/weatherforecast.read"];

  constructor(private msalService: MsalService, private broadcastService: BroadcastService) {
  }

  getIsAuthenticated() {
    return this.msalService.getAccount() != undefined;
  }

  register() {
    this.subscribeToMsalTopics();

    let scopes = this.scopes;

    this.msalService.handleRedirectCallback((redirectError: AuthError, redirectResponse: AuthResponse) => {
      if (redirectError) {
        console.error("Redirect error: ", redirectError);
        return;
      }
      console.log("Redirect success: ", redirectResponse);
    });
  }

  signIn() {
    let request: AuthenticationParameters = {
      scopes: this.scopes,
      extraQueryParameters: {}
    };
    this.msalService.loginRedirect(request);
  }

  signInFederated() {
    let request: AuthenticationParameters = {
      scopes: this.scopes,
      extraQueryParameters: { "domain_hint": environment.auth.federatedDomain }
    };
    this.msalService.loginRedirect(request);
  }
  
  signOut() {
    this.msalService.logout();
  }

  resetPassword() {
    this.handleForgotPassword();
  }

  private acquireTokenSuccessHandler(payload) {
    console.log("acquire token success " + JSON.stringify(payload));
  }

  private acquireTokenFailureHandler(payload) {
    console.log("acquire token failure " + JSON.stringify(payload));
  }

  private loginSuccessHandler(payload) {
    this.isAuthenticated = true;
    console.log("login success " + JSON.stringify(payload));
  }

  private loginFailureHandler(payload: MSALError) {
    this.isAuthenticated = false;
    console.log("login failure " + JSON.stringify(payload));
    
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

  private subscribeToMsalTopics() {
    for (let topic in MsalTopic) {
        let handlerName = topic.split(':')[1] + "Handler";
        let handler = this[handlerName];

        if (handler) {
          let subscription = this.broadcastService.subscribe(topic, handler);
          this.subscriptions.set(topic, subscription);
        }
    }
  }

  private disposeSubscriptions() {
    this.broadcastService.getMSALSubject().next(1);

    for(let topic in this.subscriptions.keys) {
      this.disposeSubscription(topic);
    }
    this.subscriptions.clear();
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