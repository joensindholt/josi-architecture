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

  identity {
    type = "SystemAssigned"
  }

  app_settings = {
    WEBSITE_RUN_FROM_PACKAGE = 1
  }

  site_config {
    always_on = false
  }
}

data "azurerm_client_config" "current" {
}

resource "azurerm_key_vault_access_policy" "josi_architecture_webapi_access_policy" {
  key_vault_id = data.azurerm_key_vault.josi_architecture_key_vault.id
  tenant_id    = azurerm_windows_web_app.josi_architecture_webapi.identity[0].tenant_id
  object_id    = azurerm_windows_web_app.josi_architecture_webapi.identity[0].principal_id

  secret_permissions = [
    "Get",
    "List"
  ]
}

output "outbound_ip_address_list" {
  value = azurerm_windows_web_app.josi_architecture_webapi.outbound_ip_address_list
}

output "identity_principal_id" {
  value = azurerm_windows_web_app.josi_architecture_webapi.identity[0].principal_id
}
