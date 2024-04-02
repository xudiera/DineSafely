@description('The geo-location for the resources.')
param location string

@description('Prefix for all resources created by this template')
param prefix string

@description('The administrator login name of a server. Can only be specified when the server is being created (and is required for creation).')
param postgreSQLAddministratorLogin string

@description('The administrator login password (required for server creation).')
@secure()
param postgreSQLAdministratorLoginPassword string

resource postgreSQL 'Microsoft.DBforPostgreSQL/flexibleServers@2023-06-01-preview' = {
  name: '${prefix}-postgresql-server'
  location: location
  tags: {
    service: prefix
  }
  sku: {
    name: 'Standard_B1ms'
    tier: 'Burstable'
  }
  properties: {
    version: '16'
    administratorLogin: postgreSQLAddministratorLogin
    administratorLoginPassword: postgreSQLAdministratorLoginPassword
    highAvailability: {
      mode: 'Disabled'
    }
    storage: {
      storageSizeGB: 32
    }
    backup: {
      geoRedundantBackup: 'Disabled'
    }
  }
  // The proper way to do this will be to add a vnet (the current budget I have don't allow me to do it.)
  // See: https://learn.microsoft.com/en-us/azure/postgresql/flexible-server/concepts-networking-private
  resource firewallAzure 'firewallRules' = {
    name: 'allow-all-azure-internal-IPs'
    properties: {
      endIpAddress: '0.0.0.0'
      startIpAddress: '0.0.0.0'
    }
  }
}

resource postgreSQLDatabase 'Microsoft.DBforPostgreSQL/flexibleServers/databases@2023-06-01-preview' = {
  parent: postgreSQL
  name: prefix
}

output postgreSQLFullyQualifiedDomainName string = postgreSQL.properties.fullyQualifiedDomainName
