@description('The geo-location for the resources')
param location string = resourceGroup().location

@description('Prefix for all resources created by this template')
param prefix string

@description('The administrator login name of a server. Can only be specified when the server is being created (and is required for creation).')
param postgreSQLAddministratorLogin string

@secure()
@description('The administrator login password (required for server creation).')
param postgreSQLAdministratorLoginPassword string

@secure()
@description('Set this to the value of your PAT token on GitHub.')
param dockerRegistryServerPassword string

//Setup resource names
var databaseName = prefix

module postgreSQL 'modules/postgreSQL.bicep' = {
  name: 'postgresql-server'
  params: {
    postgreSQLAddministratorLogin: postgreSQLAddministratorLogin
    postgreSQLAdministratorLoginPassword: postgreSQLAdministratorLoginPassword
    location: location
    prefix: prefix
  }
}

module app 'modules/app.bicep' = {
  name: '${prefix}-app-srv-plan'
  params: {
    location: location
    connectionStrings: [
      {
        connectionString: 'Server=${postgreSQL.outputs.postgreSQLFullyQualifiedDomainName};Database=${databaseName};Port=5432;User Id=${postgreSQLAddministratorLogin};Password=${postgreSQLAdministratorLoginPassword};Ssl Mode=Require;'
        name: 'DefaultConnection'
        type: 'Custom'
      }
    ]
    dockerRegistryServerPassword: dockerRegistryServerPassword
    prefix: prefix
  }
}
