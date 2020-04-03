// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  auth: {
    federatedDomain: "hillingk.onmicrosoft.com",
    redirectUri: "https://localhost:4201/",
    postLogoutRedirectUri: "https://localhost:4201/",
    authority: "https://idhack007.b2clogin.com/tfp/idhack007.onmicrosoft.com/B2C_1A_signup_signin/" //default authority is signin signup user flow
  }
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
