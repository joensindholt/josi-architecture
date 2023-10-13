variable "resource_group" {
  type = string
}

variable "location" {
  type = string
}

variable "key_vault_name" {
  type = string
}

variable "angular_app_location" {
  type = string
}

variable "webapi__outbound_ip_address_list" {
  type = list(string)
}
