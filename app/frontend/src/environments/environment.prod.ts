export const environment = {
  production: true,
  apiBaseUrl: 'https://aadb2c-aspnet-sample.azurewebsites.net/',
  auth: {
    clientId: '1f16026e-109a-4f94-8985-83fd0fcd10a6',
    federatedDomain: "hillingk.onmicrosoft.com",
    redirectUri: "https://localhost:4201/",
    authority: "https://idhack007.b2clogin.com/tfp/idhack007.onmicrosoft.com/B2C_1A_signup_signin/", //default authority is signin signup user flow
    authorityBaseUrl: "https://idhack007.b2clogin.com/tfp/idhack007.onmicrosoft.com/",
    consentScopes: ["https://idhack007.onmicrosoft.com/webapp-sample-api/weatherforecast.read"]
  }
};
