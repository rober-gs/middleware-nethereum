using System.Text.Json.Serialization;

namespace Baas.Core.DTOs
{
    public class AccountDTO
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("privateKey")]
        public string PrivateKey { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("fileJson")] 
        public string JsonFile { get; set; }
    }
}
