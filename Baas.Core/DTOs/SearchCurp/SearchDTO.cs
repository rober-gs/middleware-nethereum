using Microsoft.AspNetCore.Http;

namespace Baas.Core.DTOs
{
    public class SearchDTO
    {
        public string Curp { get; set; }            
        public string Uuid { get; set; }            
        public IFormFile file { get; set; }                                
    }
}
