using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Baas.Core.DTOs
{
    public class SearchDetailDTO
    {
        public BroadcastDTO Search { get; set; }
        public List<ParticipationDTO> Records { get; set; }
    }

    public class BroadcastDTO
    {
        [JsonPropertyName("owner")]
        public string Owner { get; set; }
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }
        [JsonPropertyName("cid")]
        public string Cid { get; set; }
        [JsonPropertyName("curp")]
        public string Curp { get; set; }
        [JsonPropertyName("create-broadcast")]
        public DateTime CreateBroadcast { get; set; }
        [JsonPropertyName("end-broadcast")]
        public DateTime EndBroadcast { get; set; }
        [JsonPropertyName("state")]
        public byte State{ get; set; }
        [JsonPropertyName("records")]
        public int Records{ get; set; }
        [JsonPropertyName("end-date")]
        public DateTime? EndDate { get; set; }
    }

    public class ParticipationDTO
    {
        [JsonPropertyName("participant")]
        public string Owner { get; set; }
        [JsonPropertyName("cid")]
        public string Cid { get; set; }
        [JsonPropertyName("create-date")]
        public DateTime CreateDate { get; set; }
    }
}