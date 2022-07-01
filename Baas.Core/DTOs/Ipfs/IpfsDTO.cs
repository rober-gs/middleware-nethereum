using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Baas.Core.DTOs
{
    public class IpfsState
    {
        [JsonPropertyName("identity")]
        public IpfsID Identity { get; set; }

        [JsonPropertyName("bandWidth")]
        public BandWidth BandWidth { get; set; }

        [JsonPropertyName("root")]
        public FileStat Root { get; set; }
    }

    public class IpfsID
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }

        [JsonPropertyName("addresses")]
        public List<string> Addresses { get; set; }

        [JsonPropertyName("agentVersion")]
        public string AgentVersion { get; set; }

        [JsonPropertyName("protocolVersion")]
        public string ProtocolVersion { get; set; }

        [JsonPropertyName("protocols")]
        public List<string> Protocols { get; set; }
    }

    public class BandWidth
    {
        [JsonPropertyName("totalIn")]
        public int TotalIn  { get; set; }

        [JsonPropertyName("totalOut")]
        public int TotalOut { get; set; }

        [JsonPropertyName("rateIn")]
        public double RateIn    { get; set; }

        [JsonPropertyName("rateOut")]
        public double RateOut   { get; set; }
    }
    public class FileStat
    {
        [JsonPropertyName("hash")]
        public string Hash  { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("cumulativeSize")]
        public int CumulativeSize   { get; set; }

        [JsonPropertyName("blocks")]
        public int Blocks   { get; set; }

        [JsonPropertyName("type")]
        public string Type  { get; set; }
    }
    
    public class Link
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("target")]
        public string Target { get; set; }
    }

    public class Object
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }
    }

    public class DirectoryList
    {
        [JsonPropertyName("entries")]
        public List<Entry> Entries { get; set; }
    }

    public class Entry
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("cid")]
        public string Hash { get; set; }
    }
}
