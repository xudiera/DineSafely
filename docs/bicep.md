# Deploy using Bicep

## Log in to Azure
`az login`

## Set variables to hold the resource group name
`resourceGroup=dinesafely-rg`

## Create the resource group
`az group create --name $resourceGroup --location 'East US'`

## Run what-if to preview the changes that will happen
`az deployment group what-if --resource-group $resourceGroup --template-file "bicep/main.bicep"`

## Deploy the Bicep file
`az deployment group create --resource-group $resourceGroup --template-file "bicep/main.bicep"`

## Review deployed resources
`az resource list --resource-group $resourceGroup`

## Clean up resources
`az group delete --name $resourceGroup`
