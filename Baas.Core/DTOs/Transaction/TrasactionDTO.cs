using System;
using System.Collections.Generic;
using System.Text;

namespace Baas.Core.DTOs
{
    public class TrasactionDTO
    {
        public string TransactionHash { get; set; }
        public int  Block { get; set; }
        public DateTime Timestamp { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Status { get; set; }
        public string TxnFee { get; set; }
        
    }    
}
