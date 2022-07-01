using System;

namespace Baas.Core.DTOs
{
    public class AttestDto
    {        
        public string IdOffchain { get; set; }
        public string Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime CreateDate { get; set; }
        public int Estatus { get; set; }
    }
}
