using System;
using System.Net;
using Baas.Core.DTOs;
using Baas.Core.Exceptions;
using Baas.Core.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace Baas.Infrastructure
{
    public class VaultServices : IVaultServices
    { 
        private readonly RestClient _client;

        private readonly int TIME_OUT = -1;
        private readonly string VAULT_URL_API;
        private readonly string VAULT_TOKEN;        
        private readonly string KV_ACCOUNT_NODE;

        public VaultServices()
        {
            _client = new RestClient();

            VAULT_TOKEN =       Environment.GetEnvironmentVariable("VAULT_TOKEN");  
            VAULT_URL_API =     Environment.GetEnvironmentVariable("VAULT_URL_API");
            KV_ACCOUNT_NODE =   Environment.GetEnvironmentVariable("VAULT_KV_ACCOUNT_NODE");            

        }

        public VaultSignerDTO GetSigner()
        {
            try
            {
                VaultDTO vault = new VaultDTO();

                Method htmlMethod = Method.Get;
                Uri baseUri = new Uri(VAULT_URL_API + KV_ACCOUNT_NODE);                

                RestRequest request = new RestRequest(baseUri, htmlMethod);
                request.AddHeader("X-Vault-Token", VAULT_TOKEN);
                request.Timeout = TIME_OUT;

                RestResponse response = _client.ExecuteAsync(request).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    vault = JsonConvert.DeserializeObject<VaultDTO>(response.Content);
                }
                else
                {
                    throw new CustomException("an error has occurred with the communication to the vault dicio " + response.Content);
                }

                VaultSignerDTO signer = new VaultSignerDTO(){
                    Mnemonic = vault.Data.Mnemonic,
                    PrivateKey = vault.Data.PrivateKey,
                    PublicKey = vault.Data.PublicKey,
                    Name = vault.Data.Name,
                };

                return signer;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception("an error has occurred with the communication to the vault dicio, err:  " + exception.Message);
            }
        }
    }
}
