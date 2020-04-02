# Getting Started

## Developer Setup

1. Install Visual Studio Code
2. Install the [Azure AD B2C custom policy extension](https://marketplace.visualstudio.com/items?itemName=AzureADB2CTools.aadb2c)
3. Install the [Azure Tools extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.vscode-node-azure-pack)
4. An Azure Subscription - to create the B2C tenant
5. [Azure Cloud Shell](https://shell.azure.com) - to execute the scripts that do a lot for you :)

### Cloud Shell

When working with this repository, you will clone within the cloud shell.

```bash
mkdir workspace && cd workspace
git clone https://github.com/kevinhillinger/azureadb2c-aspnetcore-sample.git
cd azureadb2c-aspnetcore-sample
```

Once this is done, you will be able to execute scripts within the ```./scripts``` folder.


## B2C Setup
To build this sample with custom policies in B2C, the following things needs to be done:

1. Create a B2C tenant and associate it to your subscription
2. Create the keys and application registrations in B2C for the Identity Experience Framework (to use the custom policies)
3. Create the application registration for extension attributes (custom attributes for users)
4. Create an application registration in the B2C tenant for the Angular frontend
5. Create an application registration in the B2C tenant for the Web API backend
6. Update the ```b2c/policies/appsettings.json``` with the values from steps 1-5 
7. Build the custom policies in Visual Studio Code.

# Azure AD B2C Setup

## Federating with an Azure AD Tenant

Documentation [here](https://docs.microsoft.com/en-us/azure/active-directory-b2c/identity-provider-azure-ad-single-tenant-custom?tabs=applications#register-an-application)

## 1. Create a B2C tenant and associate it to your subscription
Here are the [instructions](https://docs.microsoft.com/en-us/azure/active-directory-b2c/tutorial-create-tenant). Follow them for the B2C tenant.



## 2. Create a registration for the Angular frontend

The frontend of the app needs to be represented in b2c, i.e. b2c needs to know about it as "[relying party(https://en.wikipedia.org/wiki/Relying_party)]" application, also known as a [service provider](https://en.wikipedia.org/wiki/Service_provider_(SAML)) in the SAML days of yore.

### Instructions

Follow the steps [here](https://docs.microsoft.com/en-us/azure/active-directory-b2c/tutorial-register-applications?tabs=applications)

* Instead of naming it _webapp1_, call it: __Contoso Portal Frontend__

>This needs to represent the Angular app running in the browser. Set the clientID property in [_app.module.ts_](./app/src/app.module.ts) in the sample to the app id (client id) once this is created. 

```typescript
imports: [
    MsalModule.forRoot({ 
      clientID: '<here is where the application id goes>',
      ...
    }),
```

## 3. Create the keys and application registrations for the Identity Experience Framework

The encryption and signing keys plus the application registrations are needed to get custom policies working.

Follow the instructions [here](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-get-started?tabs=applications), but ignore the instructions where it says to create a Facebook key.

> You don't need to get the custom policy start pack, but you can look over it if you want to.

## 4. Upload the policy files

Upload the policy files in ```b2c/policies``` to B2C.

* Select the Identity Experience Framework menu item in your B2C tenant in the Azure portal.
* Select Upload custom policy.

Upload the base file first, followed by the extension files, then the "profile" files. Example:

1. TrustFrameworkBase.xml
2. TrustFrameworkExtensions.xml
3. SignUpOrSignin.xml
4. ProfileEdit.xml
5. PasswordReset.xml


https://devblogs.microsoft.com/aspnet/jwt-validation-and-authorization-in-asp-net-core/
https://developer.okta.com/blog/2018/03/23/token-authentication-aspnetcore-complete-guide
