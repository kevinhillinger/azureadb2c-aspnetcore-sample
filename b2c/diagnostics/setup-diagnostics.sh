#!/bin/bash
# PARAMETERS

while getopts s:t:u:p: option
do
case "${option}"
in
  s) target_subscription_id=${OPTARG}
      ;; # The subscription you authorize Azure AD B2C user group to configure the Azure Monitor instance 
  t) target_tenant_domain=${OPTARG}
      ;;  # The domain of the tenant that will be used to target
  u) b2c_admin_username=${OPTARG}
      ;; 
  p) b2c_admin_password=${OPTARG}
      ;; 
esac
done

if [ "$target_subscription_id" == "" ] || [ "$target_tenant_domain" == "" ];
then
  echo ""
  echo "A target subscription, tenant domain, b2c admin username + password are required! Example: "
  echo "  ./setup-diagnostics.sh \\"
  echo "    -s af8772a0-fd9c-4ddc-8ad0-7d4b3913d7dd \\"
  echo "    -t contoso.onmicrosoft.com \\"
  echo "    -u adminuser@contoso.onmicrosoft.com \\"
  echo "    -p p@ssW0rd"
  echo ""
  exit 1
fi

# az login with a B2C tenant global admin
echo "Logging in B2C tenant admin"
az login -u $b2c_admin_username -p $b2c_admin_password

# get the tenant id of the B2C tenant
b2c_tenant_id=$(az account show -o tsv --query 'managedByTenants[0].tenantId')

echo "B2C tenant = $b2c_tenant_id"

# -------------------------------------------------------------------------------------------------
# USER GROUP 
# next, create a user group to assign to the managed services definition 
# that will be created so B2C can send diagnostics to a destination (storage, monitor, event hub)
# and that can managed a targeted resource group in the target subscription
# -------------------------------------------------------------------------------------------------

user_group_name="Monitoring Management (Delegated)"
user_group_mail="monitor_mgmt_group"
user_group_desc="Monitoring management group for delegated management of the Resource Group that contains the audit log storage implementation"

# create the group and return into variable
echo "Creating user group '$user_group_name'."
user_group=$(az ad group create --display-name "$user_group_name" --mail-nickname $user_group_mail -o json)

# object id of the security group created in Azure AD B2C tenant
user_group_object_id=$(echo $user_group | jq .objectId -r)

# get current GA of the tenant and add to group owner and member
echo "Assigning tenant admin to the group as owner and a member."

az ad group owner add \
  --group $user_group_object_id \
  --owner-object-id $(az ad user show --id $(az account show -o tsv --query "user.name") -o tsv --query "objectId")

az ad group member add \
  --group $user_group_object_id \
  --member-id $(az ad user show --id $(az account show -o tsv --query "user.name") -o tsv --query "objectId")

# -------------------------------------------------------------------------------------------------
# MANAGED SERVICE SETUP
# now setup target subscription where the B2C user group has delegated Contributor on a resource group
# so we can send diagnostics to it
# -------------------------------------------------------------------------------------------------

# ! IMPORTANT ! 
az logout --username $b2c_admin_username

echo "logging out the B2C tenant admin...."
echo "Now log back in using an Owner of the target subscription (Browser window lauched)."

az login --tenant $target_tenant_domain
az account set -s $target_subscription_id  -o none

echo "Logged in as context."
az account show -o json


# create the resource group for B2C diagnostics
resource_group_name=azure-ad-b2c-monitoring

echo "creating Resource Group '$resource_group_name'."
az group create --location eastus --name $resource_group_name

# register managed service in the target subscription
echo "Registering managed services provider on the subscription."
az provider register --namespace Microsoft.ManagedServices

contributor_role_id=b24988ac-6180-42a0-ab88-20f7382dd24c
managed_services_definition=$(az managedservices definition create \
  --name 'Azure AD B2C Managed Services' \
  --description 'Enables Azure Monitor in Azure AD B2C.' \
  --tenant-id $b2c_tenant_id \
  --role-definition-id $contributor_role_id \
  --principal-id $user_group_object_id \
  --output json)

echo "Managed Services Definition created for 'Azure AD B2C Managed Services'."
echo $managed_services_definition | jq .
echo ""

az managedservices assignment create \
  --definition $(echo $managed_services_definition | jq .id -r) \
  -g $resource_group_name \
  -o json

  echo "B2C managed services definition assigned to Resource Group '$resource_group_name'."
  echo ""
  
  echo "Done."