using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Baas.Core.DTOs
{
    public class AddAccountDTO
    {
        [JsonPropertyName("id")]
        public int UserId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string password { get; set; }

    }
}
