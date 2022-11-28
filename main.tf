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

resource "azurerm_service_plan" "josi-architecture-service-plan" {
  name                = "josi-architecture-service-plan"
  resource_group_name = "josi-architecture-resource-group"
  location            = "North Europe"
  os_type             = "Windows"
  sku_name            = "F1"
}
