using Baas.Core.DTOs;
using Baas.Core.Exceptions;
using Baas.Core.Interfaces;
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Hex.HexTypes;
using Nethereum.KeyStore;
using Nethereum.KeyStore.Model;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Numerics;
using System.Text;

using System.Threading.Tasks;


namespace Baas.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IVaultServices _vaultService;
        private readonly Web3Service _middleware;
        private readonly Account _account;
        private readonly VaultSignerDTO _signer;

        public AccountService(IVaultServices vaultServices)
        {
            _vaultService = vaultServices;
            _signer = _vaultService.GetSigner();
            _account = new Account(_signer.PrivateKey, Chain.Rinkeby);
            _middleware = new Web3Service(_account);
        }

        public async Task<AccountDTO> AccountInfoAsync()
        {
            try
            {

                HexBigInteger balance = await _middleware.Web3.Eth.GetBalance.SendRequestAsync(_signer.PublicKey);
                
                AccountDTO dataMapping = new AccountDTO
                {
                    Address = _signer.PublicKey,
                    Name = _signer.Name,
                    Balance = Web3.Convert.FromWei(balance.Value)
                };

                return dataMapping;

            }
            catch (Exception exception)
            {
                throw new NotImplementedException(exception.Message);
            }
        }


    }
}
