

# Getting Started

## B2C Tenant Setup

Follow the instructions [here](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-get-started?tabs=applications) to get the B2C tenant configured for custom policies.


## Starter pack

Clone the starter pack of policies if you want to see default setup.

```git
git clone https://github.com/Azure-Samples/active-directory-b2c-custom-policy-starterpack
```

# Policies

## Force a password reset

[https://github.com/azure-ad-b2c/samples/tree/master/policies/force-password-reset-after-90-days](https://github.com/azure-ad-b2c/samples/tree/master/policies/force-password-reset-after-90-days)

## Custom Email Verification (SendGrid)

This sample sends email verifications using SendGrid. If you haven't already, you'll need to setup the following:

1. Create a [SendGrid account](https://docs.microsoft.com/en-us/azure/sendgrid-dotnet-how-to-send-email#create-a-sendgrid-account)
2. Create a [SendGrid API Key](https://docs.microsoft.com/en-us/azure/sendgrid-dotnet-how-to-send-email#to-find-your-sendgrid-api-key)
3. Create the [email template](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-email#create-sendgrid-template) in SendGrid
4. Create a [Policy Key in B2C](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-email#create-azure-ad-b2c-policy-key)


[Here](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-email) is the official documentation for setting it up for B2C.

# Backend

## B2C Integration of Web API 

### Client Certificates
https://docs.microsoft.com/en-us/azure/app-service/app-service-web-configure-tls-mutual-auth
