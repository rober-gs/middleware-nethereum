using System;
using System.Text.Json.Serialization;

namespace Baas.Core.DTOs
{
    public class AddAttestationDTO
    {
        [JsonPropertyName("eth_account")]   public string   Account     { get; set; }
        [JsonPropertyName("id_offchain")]   public string   IdOffchain  { get; set; }
        [JsonPropertyName("value")]         public string   Value       { get; set; }
        [JsonPropertyName("time_stamp")]    public DateTime TimeStamp   { get; set; }
        [JsonPropertyName("status")]        public int      Status      { get; set; }
    }
}