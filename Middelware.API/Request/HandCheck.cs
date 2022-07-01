using System;
using System.Text.Json.Serialization;

namespace Middelware.Request
{

    public class HandCheckDTO
    {
        [JsonPropertyName("environment")]
        public string? ASPNETCOREENVIRONMENT { get; set; }       

        [JsonPropertyName("test")]
        public string? TESTENV { get; set; }

        [JsonPropertyName("blockchian")]
        public BlockchainEnvironments? Blockchain { get; set; }

        [JsonPropertyName("ipfs")]
        public IpfsEvironments? Ipfs { get; set; }

        [JsonPropertyName("vault")]
        public VaultEnvironments? Vault { get; set; }

    }

    public class BlockchainEnvironments
    {
        [JsonPropertyName("NODE_RCP_URL")]
        public string? NODERCPURL { get; set; }        

        [JsonPropertyName("SMARTCONTRACT_ADDRESS_SEARCH_CURP")]
        public string? SMARTCONTRACTADDRESSSEARCHCURP { get; set; }
    }
    
    public class IpfsEvironments
    {
        [JsonPropertyName("IPFS_SERVER_BASE_URL")]
        public string? IPFSSERVERBASEURL { get; set; }

        [JsonPropertyName("IPFS_SERVER_PORT")]
        public string? IPFSSERVERPORT { get; set; }

        [JsonPropertyName("IPFS_API_VERSION")]
        public string? IPFSAPIVERSION { get; set; }
    }

    public class VaultEnvironments
    {
        [JsonPropertyName("VAULT_URL_API")]
        public string? VAULTURLAPI { get; set; }

        [JsonPropertyName("VAULT_TOKEN")]
        public string? VAULTTOKEN { get; set; }

        [JsonPropertyName("VAULT_KV_ACCOUNT_NODE")]
        public string? VAULTKVACCOUNTNODE { get; set; }

    }
    public class Example
    {
        [JsonPropertyName("curp")]
        public string? Curp  { get; set; }
        [JsonPropertyName("transaction_code")]
        public string? Transaction_code { get; set; }
        [JsonPropertyName("embedding")]
        public List<double>? Embedding { get; set; }
        [JsonPropertyName("ip_to_response")]
        public string? Ip_to_response { get; set; }
    }

}

