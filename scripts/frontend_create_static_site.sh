#!/bin/bash

storage_account_name=b2csample007frontend
resource_group=azure-ad-b2c-sample-frontend
location=eastus

echo "Creating resource group."
az group create -n $resource_group -l $location

echo "Creating storage account."
az storage account create -n $storage_account_name \
    -g $resource_group \
    -l $location \
    --sku Standard_LRS

echo "Updating storage to support static hosting."
az storage blob service-properties update \
    --account-name $storage_account_name \
    --static-website \
    --404-document index.html \
    --index-document index.html

# build the frontend and deploy

echo "Checking if angular CLI is installed."

package='@angular/cli'
if [ `npm list -g | grep -c $package` -eq 0 ]; then
    echo "Installing angular CLI."
    npm install -g $package --no-shrinkwrap
fi

# build
echo "Building frontend."
pushd ../app/frontend && ng build --prod && popd

# deploy
echo "Deploying angular app to blob storage."
az storage blob upload-batch \
    -s ../app/frontend/dist \
    -d \$web \
    --account-name $storage_account_name 

echo ""
echo "Website URL: " + "$(az storage account show -n $storage_account_name -g $resource_group -o tsv --query 'primaryEndpoints.web')"
echo ""
echo "Done."