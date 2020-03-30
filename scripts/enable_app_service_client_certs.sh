#!/bin/bash

app_name="<the web app name>"
resource_group="<the resource group>"
az webapp update --set clientCertEnabled=true --name $app_name --resource-group $resource