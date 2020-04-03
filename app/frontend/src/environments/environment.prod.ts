export const environment = {
  production: true,
  apiBaseUrl: 'https://aadb2c-aspnet-sample.azurewebsites.net/',
  auth: {
    federatedDomain: "hillingk.onmicrosoft.com",
    redirectUri: "https://b2csample007frontend.z13.web.core.windows.net/",
    postLogoutRedirectUri: "https://b2csample007frontend.z13.web.core.windows.net/",
    authority: "https://idhack007.b2clogin.com/tfp/idhack007.onmicrosoft.com/B2C_1A_signup_signin/" //default authority is signin signup user flow
  }
};
