include "root" {
  path = find_in_parent_folders()
}

dependencies {
  paths = ["../api", "../frontend"]
}

dependency "api" {
  config_path = "../api"
  mock_outputs = {
      outbound_ip_address_list = []
  }
}

inputs = {
  josi_architecture_webapi__outbound_ip_address_list = dependency.api.outputs.outbound_ip_address_list
}