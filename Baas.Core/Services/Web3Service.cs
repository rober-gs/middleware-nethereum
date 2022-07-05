using System;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Baas.Core
{
    public class Web3Service
    {
        private readonly string NODE_RCP_URL;        
        public readonly Web3 Web3;
        public ContractHandler Handler { get; }
        
        public Web3Service(string address, Account signer)
        {            
            NODE_RCP_URL = Environment.GetEnvironmentVariable("NODE_RCP_URL");

            Web3 = new Web3(signer, NODE_RCP_URL);
            //_web3.TransactionManager.UseLegacyAsDefault = true;
            Handler = Web3.Eth.GetContractHandler(address);
        }
        
        public Web3Service(Account signer)
        {            
            NODE_RCP_URL = Environment.GetEnvironmentVariable("NODE_RCP_URL");
            Web3 = new Web3(signer, NODE_RCP_URL);
        }
    }    
}