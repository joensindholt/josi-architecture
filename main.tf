terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.33.0"
    }
  }

  backend "azurerm" {
    resource_group_name  = "josi-architecture-resource-group"
    storage_account_name = "josiarchitecturestorage"
    container_name       = "josi-architecture-terraform-state"
    key                  = "prod.terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}


variable "resource_group" {
  type    = string
  default = "josi-architecture-resource-group"
}

variable "location" {
  type    = string
  default = "North Europe"
}

data "azurerm_key_vault" "josi_architecture_key_vault" {
  name                = "josi-arch-key-vault"
  resource_group_name = var.resource_group
}

data "azurerm_key_vault_secret" "sql_server_administrator_login" {
  name         = "sql-server-administrator-login"
  key_vault_id = data.azurerm_key_vault.josi_architecture_key_vault.id
}

data "azurerm_key_vault_secret" "sql_server_administrator_password" {
  name         = "sql-server-administrator-password"
  key_vault_id = data.azurerm_key_vault.josi_architecture_key_vault.id
}

resource "azurerm_mssql_server" "josi_architecture_sql_server" {
  name                         = "josi-architecture-sql-server"
  resource_group_name          = var.resource_group
  location                     = var.location
  administrator_login          = data.azurerm_key_vault_secret.sql_server_administrator_login.value
  administrator_login_password = data.azurerm_key_vault_secret.sql_server_administrator_password.value
  minimum_tls_version          = "1.2"
  version                      = "12.0"
}

resource "azurerm_mssql_database" "josi_architecture_sql_database" {
  name                 = "josi-architecture-sql-database"
  server_id            = azurerm_mssql_server.josi_architecture_sql_server.id
  collation            = "Danish_Norwegian_CI_AS"
  sku_name             = "Basic"
  storage_account_type = "Local"
}

resource "azurerm_service_plan" "josi_architecture_service_plan" {
  name                = "josi-architecture-service-plan"
  resource_group_name = var.resource_group
  location            = var.location
  os_type             = "Windows"
  sku_name            = "F1"
}

resource "azurerm_windows_web_app" "josi_architecture_webapi" {
  name                = "josi-architecture-webapi"
  resource_group_name = var.resource_group
  location            = var.location
  service_plan_id     = azurerm_service_plan.josi_architecture_service_plan.id

  app_settings = {
    WEBSITE_RUN_FROM_PACKAGE                               = 1
    ASPNETCORE_ConnectionStrings__JosiArchitectureDatabase = "Server=tcp:${azurerm_mssql_server.josi_architecture_sql_server.name}.database.windows.net,1433;Initial Catalog=${azurerm_mssql_database.josi_architecture_sql_database.name};Persist Security Info=False;User ID=${data.azurerm_key_vault_secret.sql_server_administrator_login.value};Password=${data.azurerm_key_vault_secret.sql_server_administrator_password.value};MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }

  site_config {
    always_on = false
  }
}
