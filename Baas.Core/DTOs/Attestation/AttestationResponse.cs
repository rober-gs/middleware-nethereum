using System;
using System.Text.Json.Serialization;

namespace Baas.Core.DTOs
{

    public class AttestationResponse
    {
        [JsonPropertyName("attestationData")]   public AttestationData Attestation { get; set; }
        [JsonPropertyName("blockchainData")]    public BlockchainData Blockchain { get; set; }
    }

    
    public class AttestationData
    {
        [JsonPropertyName("indexAttestation")]  public long     IndexAttestation    { get; set; }
        [JsonPropertyName("publicKey")]         public string   AccountAttestation  { get; set; }
        [JsonPropertyName("idOffchain")]        public string   IdOffchain          { get; set; }
        [JsonPropertyName("value")]             public string   Value               { get; set; }
        [JsonPropertyName("timeStamp")]         public DateTime TimeStamp           { get; set; }
        [JsonPropertyName("status")]            public int      Status              { get; set; }
        [JsonPropertyName("createDate")]        public DateTime CreateDate          { get; set; }        
    }
}