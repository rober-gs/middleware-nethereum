using System.Text.Json.Serialization;

namespace Baas.Core.DTOs
{
    public class BlockchainData
    {
        [JsonPropertyName("gasUsed")] public string GasUsed { get; set; }
        [JsonPropertyName("transactionHash")] public string TransactionHash { get; set; }
        [JsonPropertyName("blockNumber")] public long BlockNumber { get; set; }
        [JsonPropertyName("blockHash")] public string BlockHash { get; set; }
        [JsonPropertyName("status")] public long Status { get; set; }
    }
}
