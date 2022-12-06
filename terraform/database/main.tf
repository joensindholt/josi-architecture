variable "resource_group" {
  type = string
}

variable "location" {
  type = string
}

variable "josi_architecture_webapi__outbound_ip_address_list" {
  type = list(string)
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

resource "azurerm_mssql_firewall_rule" "josi_architecture_webapi_firewall_rule" {
  for_each = toset(var.josi_architecture_webapi__outbound_ip_address_list)

  name             = "josi-architecture-webapi-firewall-rule-${index(var.josi_architecture_webapi__outbound_ip_address_list, each.key)}"
  server_id        = azurerm_mssql_server.josi_architecture_sql_server.id
  start_ip_address = each.key
  end_ip_address   = each.key
}
