namespace Baas.Core.DTOs
{
    public class VaultDTO
    {        
        public string RequestId { get; set; }
        
        public string LeaseId { get; set; }
        
        public bool Renewable { get; set; }
        
        public int LeaseDuration { get; set; }
        
        public Data Data { get; set; }
        
        public object WrapInfo { get; set; }
        
        public object Warnings { get; set; }
        
        public object Auth { get; set; }
    }
    public class Data
    {        
        public string Mnemonic { get; set; }        
        public string PrivateKey { get; set; }        
        public string PublicKey { get; set; }
    }

    public class VaultSignerDTO
    {        
        public string Mnemonic { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }

    }

}
