terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.33.0"
    }
  }

  backend "azurerm" {
    container_name       = "josi-architecture-terraform-state"
    resource_group_name  = "josi-architecture-resource-group"
    storage_account_name = "josiarchitecturestorage"
  }
}

provider "azurerm" {
  features {}
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


