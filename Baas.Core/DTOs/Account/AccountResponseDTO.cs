using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Baas.Core.DTOs
{
    public class AccountResponseDTO
    {
        [JsonPropertyName("accountData")] public AccountData AccountData { get; set; }
        [JsonPropertyName("blockchainData")] public BlockchainData Blockchain { get; set; }
    }

    public class AccountData 
    {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("idOffChain")] public long OffChainID { get; set; }
        [JsonPropertyName("address")] public string Address { get; set; }
        [JsonPropertyName("keyStore")] public string KeyStore { get; set; }
    
    }

    public class AccountListDto
    {
        [JsonPropertyName("idOffChain")] public long OffChainID { get; set; }
        [JsonPropertyName("address")]    public string Address { get; set; }
        [JsonPropertyName("name")]       public string Name { get; set; }
        [JsonPropertyName("lastName")]   public string LastName { get; set; }
        [JsonPropertyName("email")]      public string Email { get; set; }
        [JsonPropertyName("datetime")]   public DateTime Date { get; set; }
        [JsonPropertyName("keyfile")]   public string Keyfile { get; set; }
    }




}
