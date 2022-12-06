generate "provider" {
  path = "provider.tf"
  if_exists = "overwrite_terragrunt"
  contents = <<EOF
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.33.0"
    }
  }
}

provider "azurerm" {
  features {}
}
EOF
}

remote_state {
  backend = "azurerm"
  generate = {
    path      = "backend.tf"
    if_exists = "overwrite_terragrunt"
  }
  config = {
    resource_group_name  = "josi-architecture-resource-group"
    storage_account_name = "josiarchitecturestorage"
    container_name       = "josi-architecture-terraform-state"
    key                  = "${path_relative_to_include()}/terraform.tfstate"
  }
}

inputs = {
  key_vault_name       = "josi-arch-key-vault"
  resource_group       = "josi-architecture-resource-group"
  location             = "North Europe"
  angular_app_location = "westeurope"
}