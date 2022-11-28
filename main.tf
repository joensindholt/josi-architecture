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

resource "azurerm_windows_web_app" "josi-architecture-webapi" {
  name                = "josi-architecture-webapi"
  resource_group_name = azurerm_service_plan.josi-architecture-service-plan.resource_group_name
  location            = azurerm_service_plan.josi-architecture-service-plan.location
  service_plan_id     = azurerm_service_plan.josi-architecture-service-plan.id

  site_config {
    always_on = false
  }
}

resource "azurerm_linux_web_app_slot" "josi-architecture-webapi-development-slot" {
  name           = "development"
  app_service_id = azurerm_windows_web_app.architecture.id

  site_config {}
}

resource "azurerm_linux_web_app_slot" "josi-architecture-webapi-production-slot" {
  name           = "production"
  app_service_id = azurerm_windows_web_app.architecture.id

  site_config {}
}
