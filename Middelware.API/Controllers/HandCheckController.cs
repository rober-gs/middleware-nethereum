using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Middelware.Request;
using Newtonsoft.Json;

namespace Middelware.Controllers
{
    [Produces("application/json")]
    [Route("api/handCheck")]
    [ApiController]
    public class HandCheckController : ControllerBase
    {

        private readonly ILogger<HandCheckController> _logger;

        public HandCheckController(ILogger<HandCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet("data")]
        public Example GetData()
        {

            string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string fullPath = Path.Combine(directory, "Request/examplesRequest.json");

            List<Example> items;
            
            using (StreamReader r = new StreamReader(fullPath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Example>>(json);
            }

            Random rnd = new Random();
            int index = rnd.Next(0, items.Count);

            return items[index];
        }


        [HttpGet("checkEnvironments")]
        public HandCheckDTO GetEnviroments()
        {
            _logger.LogInformation("Check Environments! ");

            string? ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string? VAULT_URL_API = Environment.GetEnvironmentVariable("VAULT_URL_API");
            string? VAULT_TOKEN = Environment.GetEnvironmentVariable("VAULT_TOKEN");
            string? VAULT_KV_ACCOUNT_NODE = Environment.GetEnvironmentVariable("VAULT_KV_ACCOUNT_NODE");
            string? NODE_RCP_URL = Environment.GetEnvironmentVariable("NODE_RCP_URL");
            string? IPFS_SERVER_BASE_URL = Environment.GetEnvironmentVariable("IPFS_SERVER_BASE_URL");
            string? IPFS_SERVER_PORT = Environment.GetEnvironmentVariable("IPFS_SERVER_PORT");
            string? IPFS_API_VERSION = Environment.GetEnvironmentVariable("IPFS_API_VERSION");
            string? SMARTCONTRACT_ADDRESS_SEARCH_CURP = Environment.GetEnvironmentVariable("SMARTCONTRACT_ADDRESS_SEARCH_CURP");
            string? TEST_ENV = Environment.GetEnvironmentVariable("TEST_ENV");


            BlockchainEnvironments blockchain = new BlockchainEnvironments
            {
                NODERCPURL = NODE_RCP_URL,
                SMARTCONTRACTADDRESSSEARCHCURP = SMARTCONTRACT_ADDRESS_SEARCH_CURP,
            };

            IpfsEvironments ipfs = new IpfsEvironments
            {
                IPFSSERVERBASEURL = IPFS_SERVER_BASE_URL,
                IPFSSERVERPORT = IPFS_SERVER_PORT,
                IPFSAPIVERSION = IPFS_API_VERSION,
            };

            VaultEnvironments vault = new VaultEnvironments
            {
                VAULTURLAPI = VAULT_URL_API,
                VAULTTOKEN = VAULT_TOKEN,
                VAULTKVACCOUNTNODE = VAULT_KV_ACCOUNT_NODE,
            };


            HandCheckDTO checkDto = new HandCheckDTO
            {
                ASPNETCOREENVIRONMENT = ASPNETCORE_ENVIRONMENT,                
                TESTENV = TEST_ENV,
                Blockchain = blockchain,
                Ipfs = ipfs,
                Vault = vault
            };

            return checkDto;
        }
    }
}

