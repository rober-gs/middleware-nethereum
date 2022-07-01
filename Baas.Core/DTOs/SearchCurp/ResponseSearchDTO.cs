
using System.Text.Json.Serialization;

namespace Baas.Core.DTOs
{
    public class ResponseCurpDTO
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("ipfs-data")]
        public Entry IpfsData { get; set; }

        [JsonPropertyName("blockchain-data")]
        public BlockchainData BlockchainData { get; set; }        
    }
}