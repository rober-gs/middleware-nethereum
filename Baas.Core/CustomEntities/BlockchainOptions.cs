using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Baas.Core.CustomEntities
{
    public class BlockchainOptions
    {
        [JsonPropertyName("urlNodo")]
        public string UrlNodo { get; set; }
        [JsonPropertyName("privateKey")]
        public string PrivateKey { get; set; }
        [JsonPropertyName("addressDicio")]
        public string AddressDicio { get; set; }
        [JsonPropertyName("attestationAddress")]
        public string AttestationAddress { get; set; }
    }

}
