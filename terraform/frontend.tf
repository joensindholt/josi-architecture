resource "azurerm_static_site" "josi_architecture_angular_site" {
  name                = "josi-architecture-angular-site"
  resource_group_name = var.resource_group
  location            = var.angular_app_location
}

output "angular_site_api_key" {
  description = "The API key of the Angular Static Web App, which is used for later interacting with this Static Web App from other clients, e.g. GitHub Action."
  value       = azurerm_static_site.josi_architecture_angular_site.api_key
}
