@description('The geo-location for the resources.')
param location string

@description('Prefix for all resources created by this template')
param prefix string

@description('The connection string to the database')
param connectionStrings array

@secure()
@description('Set this to the value of your PAT token on GitHub.')
param dockerRegistryServerPassword string

resource appServicePlan 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: '${prefix}-app-srv-plan'
  location: location
  tags: {
    service: prefix
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
  sku: {
    name: 'F1'
    tier: 'Free'
  }
}

resource webApp 'Microsoft.Web/sites@2023-01-01' = {
  name: prefix
  location: location
  tags: {
    service: prefix
  }
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      http20Enabled: true
      linuxFxVersion: 'DOCKER|${'ghcr.io/xudiera/dinesafely'}'
      connectionStrings: connectionStrings
      appSettings: [
        {
          name: 'DOCKER_REGISTRY_SERVER_URL'
          value: 'https://ghcr.io'
        }
        {
          name: 'DOCKER_REGISTRY_SERVER_USERNAME'
          value: 'xudiera'
        }
        {
          name: 'DOCKER_REGISTRY_SERVER_PASSWORD'
          value: dockerRegistryServerPassword
        }
        {
          name: 'WEBSITES_PORT'
          value: '8080'
        }
      ]
    }
  }
}
