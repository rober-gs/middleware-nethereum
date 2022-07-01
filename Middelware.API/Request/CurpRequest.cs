using System.Text.Json.Serialization;

namespace Middelware;

    public class CurpRequest 
    {
            [JsonPropertyName("curp")]
            public string Curp { get; set; }
            [JsonPropertyName("uuid")]
            public string Uuid { get; set; }
            [JsonPropertyName("json")]
            public IFormFile File { get; set; }            
    }    

