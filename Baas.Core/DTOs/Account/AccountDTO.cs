using System.Text.Json.Serialization;

namespace Baas.Core.DTOs
{
    public class AccountDTO
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
