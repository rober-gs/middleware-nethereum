using System;
using Baas.Core.Interfaces;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Baas.Infrastructure.BlockchainServices
{
    public class NethereumMiddleware : INethereumMiddleware
    {
        private readonly string NODE_RCP_URL;

        public NethereumMiddleware()
        {
            NODE_RCP_URL = Environment.GetEnvironmentVariable("NODE_RCP_URL");
        }

        public Web3 ConnectNodo()
        {
            return new Web3(NODE_RCP_URL);
        }

        public Web3 ConnectNodo(Account account)
        {           
            return new Web3(account);
        }

        public Web3 ConnectNodo(Account account, string urlNode)
        {
            return new Web3(account, urlNode);
        }
    }
}