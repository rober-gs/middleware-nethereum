using System;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Baas.Core
{
    public class Web3Service
    {
        private readonly string NODE_RCP_URL;        
        private readonly Web3 _web3;
        public ContractHandler Handler { get; }

        

        public Web3Service(string address, Account signer)
        {            
            NODE_RCP_URL = Environment.GetEnvironmentVariable("NODE_RCP_URL");

            _web3 = new Web3(signer, NODE_RCP_URL);
            //_web3.TransactionManager.UseLegacyAsDefault = true;
            Handler = _web3.Eth.GetContractHandler(address);
        }
    }    
}