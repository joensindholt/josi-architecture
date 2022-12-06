variable "resource_group" {
  type = string
}

variable "location" {
  type = string
}

variable "key_vault_name" {
  type = string
}

data "azurerm_key_vault" "josi_architecture_key_vault" {
  name                = var.key_vault_name
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
    WEBSITE_RUN_FROM_PACKAGE = 1
  }

  site_config {
    always_on = false
  }
}

output "outbound_ip_address_list" {
  value = azurerm_windows_web_app.josi_architecture_webapi.outbound_ip_address_list
}

# data "azurerm_client_config" "current" {}

# resource "azurerm_key_vault_access_policy" "josi_architecture_webapi_access_policy" {
#   key_vault_id = data.azurerm_key_vault.josi_architecture_key_vault.id
#   tenant_id    = data.azurerm_client_config.current.tenant_id
#   object_id    = azurerm_windows_web_app.josi_architecture_webapi.id

#   secret_permissions = [
#     "Get",
#   ]
# }

# resource "azurerm_mssql_firewall_rule" "josi_architecture_webapi_firewall_rule" {
#   for_each = toset(azurerm_windows_web_app.josi_architecture_webapi.outbound_ip_address_list)

#   name             = "josi-architecture-webapi-firewall-rule-${index(azurerm_windows_web_app.josi_architecture_webapi.outbound_ip_address_list, each.key)}"
#   server_id        = azurerm_mssql_server.josi_architecture_sql_server.id
#   start_ip_address = each.key
#   end_ip_address   = each.key
# }
