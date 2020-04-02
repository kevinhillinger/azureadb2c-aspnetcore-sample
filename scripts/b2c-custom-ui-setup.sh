#!/bin/bash

storage_account_name=b2csample007ui
storage_container=b2c
resource_group=azure-ad-b2c-sample-frontend
location=eastus

echo "Creating resource group."
az group create -n $resource_group -l $location  --output none

echo "Creating storage account."

az storage account create -n $storage_account_name \
    -g $resource_group \
    -l $location \
    --sku Standard_LRS \
    --output none

echo "Creating storage container."
az storage container create \
    --auth-mode login \
    --account-name $storage_account_name \
    --name $storage_container \
    --public-access blob \
    --output none

echo "Updating to allow for CORS."
az storage cors add \
    --methods GET OPTIONS \
    --origins 'https://idhack007.b2clogin.com'
    --services blob \
    --account-key $(az storage account keys list --account-name $storage_account_name --resource-group $resource_group --output tsv --query '[?keyName == `key1`].value') \
    --account-name $storage_account_name \
    --allowed-headers * 

echo "Deploying custom ui."
az storage blob upload-batch \
    -s ../b2c/ui \
    -d $storage_container  \
    --account-name $storage_account_name 