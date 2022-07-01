using Baas.Core.DTOs;
using Baas.Core.Exceptions;
using Baas.Core.Interfaces;
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.KeyStore;
using Nethereum.KeyStore.Model;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Baas.Core.BlockchainDtos.AccountFunctions;

//namespace Baas.Core.Services
//{
//    public class AccountService : IAccountService
//    {
//        INethereumMiddleware _nethereum;
//        IVaultServices _vaultService;
//        private readonly ContractHandler _contractHandler;
//        private readonly string _smartContract;

//        public AccountService(INethereumMiddleware nethereum, IVaultServices vaultServices)
//        {
//            _nethereum = nethereum;
//            _smartContract = "0xA8496e18c0b5Edaa8038BfCa7E2710f24A6f92BE";

//            _contractHandler = _nethereum.ConnectNodo().Eth.GetContractHandler(_smartContract);

//            _vaultService = vaultServices;
//        }

//        /// <summary>
//        /// Genera una KeyStore (Cuenta blockchain fuera del nodo.)
//        /// </summary>
//        /// <param name="password">password para generar cuenta. </param>
//        /// <returns></returns>
//        public AccountDTO GenerateAccount(string password)
//        {
//            if (string.IsNullOrWhiteSpace(password) || password.Length < 8) throw new CustomException("Uses 8 or more characters with a combination of letters, numbers and symbols");

//            KeyStoreScryptService keyStoreService = new KeyStoreScryptService();
//            ScryptParams scryptParams = new ScryptParams { Dklen = 32, N = 32, R = 1, P = 8 };

//            EthECKey ecKey = EthECKey.GenerateKey();

//            KeyStore<ScryptParams> keyStore = keyStoreService.EncryptAndGenerateKeyStore(password,
//                                                                        ecKey.GetPrivateKeyAsBytes(),
//                                                                        ecKey.GetPublicAddress(),
//                                                                        scryptParams);

//            string json = keyStoreService.SerializeKeyStoreToJson(keyStore);

//            //
//            // Ejemplo para desencrypar desde el Json. 
//            //var key = keyStoreService.DecryptKeyStoreFromJson(password, json);

//            return new AccountDTO
//            {
//                Address = ecKey.GetPublicAddress(),
//                PrivateKey = ecKey.GetPrivateKey(),
//                Password = password,
//                JsonFile = json,
//            };
//        }
//        /// <summary>
//        /// Genera una cuenta administrada con el nodo. 
//        /// </summary>
//        /// <param name="dto">password de la cuenta</param>
//        /// <returns></returns>
//        public async Task<AccountResponseDTO> AddAccountAsync(AddAccountDTO dto)
//        {
//            if (string.IsNullOrWhiteSpace(dto.password) || dto.password.Length < 8) throw new CustomException("Uses 8 or more characters with a combination of letters, numbers and symbols");

            
//            AccountDTO account = GenerateAccount(dto.password);
//            try
//            {              
//                bool StoreKey = _vaultService.PutKeyValue(account);
//            }
//            catch (Exception)
//            {
                
//            }

//            var addAccount = new AddAccountFunction
//            {
//                IdOffChain = dto.UserId,
//                AccountEth = account.Address,
//                FieldOne = Encoding.ASCII.GetBytes(dto.Name),
//                FieldTwo = Encoding.ASCII.GetBytes(dto.LastName),
//                FieldThree = Encoding.ASCII.GetBytes(dto.Email),
//                Status = 1
//            };

//            var tx = await _contractHandler.SendRequestAndWaitForReceiptAsync(addAccount, null);

//            var eventAdd = tx.DecodeAllEvents<NewAccountEventDTO>()[0].Event;

//            BlockchainData blockchainData = new BlockchainData
//            {
//                BlockNumber = (long)tx.BlockNumber.Value,
//                GasUsed = tx.GasUsed.ToString(),
//                TransactionHash = tx.TransactionHash
//            };
//            AccountData acountData = new AccountData
//            {
//                Id = (long)eventAdd.IdBlockchain,
//                OffChainID = (long)eventAdd.IdDicio,
//                Address = account.Address,
//                KeyStore = account.JsonFile,
//            };

//            return new AccountResponseDTO
//            {
//                AccountData = acountData,
//                Blockchain = blockchainData
//            };
//        }
//        public async Task<List<AccountListDto>> GetAccountList(string filter)
//        {
//            List<AccountListDto> data = new List<AccountListDto>();

//            var count = await QueryAccountsAsync();
//            if (count > 0)
//            {
//                for (int i = 0; i <= count; i++)
//                {
//                    var item = await GetAccountAsync(i);
//                    if (item.OffChainID != 0 || !string.IsNullOrEmpty(item.Address) )                        
//                    {
//                        data.Add(item);
//                    }                    
//                }
//            }

//            return data;
//        }
//        public async Task<AccountListDto> GetAccountByOffchainIDAsync(int idOffchain)
//        {
//            try
//            {
//                string keyStore = string.Empty;

//                if (idOffchain == 0) throw new CustomException("Is requerid");

//                var getAccountIdOffchainFunction = new GetAccountIdOffchainFunction()
//                {
//                    IdOffChain = idOffchain
//                };

//                var OutputDTO = await _contractHandler.QueryDeserializingToObjectAsync<GetAccountIdOffchainFunction, GetAccountIdOffchainOutputDTO>(getAccountIdOffchainFunction);

//                try
//                {
//                    var query = _vaultService.GetKeyValue(OutputDTO.ReturnValue2).Data;
                    
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("errror al leer el key file: "+ e.Message);
//                }

//                return new AccountListDto
//                {
//                    OffChainID = (long)OutputDTO.ReturnValue1,
//                    Address = OutputDTO.ReturnValue2,
//                    Name = OutputDTO.ReturnValue3,
//                    LastName = OutputDTO.ReturnValue4,
//                    Email = OutputDTO.ReturnValue5,
//                    Date = UnixTimeToDateTime(OutputDTO.ReturnValue6),
//                    Keyfile = keyStore
//                };
//            }
//            catch (Exception exception)
//            {
//                Console.WriteLine(exception.Message);
//                return new AccountListDto();
//            }
//        }
//        public async Task<AccountListDto> GetAccountEthAsync(string address)
//        {
//            try
//            {
//                string keyStore = string.Empty;

//                if (string.IsNullOrWhiteSpace(address) || address.Length < 32) throw new CustomException("Uses 8 or more characters with a combination of letters, numbers and symbols");

//                var getAccountEthFunction = new GetAccountEthFunction
//                {
//                    EthAccount = address
//                };

//                var OutputDTO = await _contractHandler.QueryDeserializingToObjectAsync<GetAccountEthFunction, GetAccountEthOutputDTO>(getAccountEthFunction);

//                try
//                {
//                    var query = _vaultService.GetKeyValue(OutputDTO.ReturnValue2).Data;
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("errror al leer el key file: " + e.Message);
//                }

//                return new AccountListDto
//                {
//                    OffChainID = (long)OutputDTO.ReturnValue1,
//                    Address = OutputDTO.ReturnValue2,
//                    Name = OutputDTO.ReturnValue3,
//                    LastName = OutputDTO.ReturnValue4,
//                    Email = OutputDTO.ReturnValue5,
//                    Date = UnixTimeToDateTime(OutputDTO.ReturnValue6),
//                    Keyfile = keyStore,
//                };
//            }
//            catch (Exception exception)
//            {
//                Console.WriteLine(exception.Message);
//                return new AccountListDto();
//            }
//        }        
//        private async Task<AccountListDto> GetAccountAsync(int idAccount)
//        {
//            try
//            {
//                string keyStore = string.Empty;

//                if (idAccount == 0) throw new CustomException("Is requerid");

//                var getAccountFunction = new GetAccountFunction
//                {
//                    Idaccount = idAccount
//                };

//                var OutputDTO = await _contractHandler.QueryDeserializingToObjectAsync<GetAccountFunction, GetAccountOutputDTO>(getAccountFunction);
               
//                try
//                {
//                    var query = _vaultService.GetKeyValue(OutputDTO.ReturnValue2).Data;
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("errror al leer el key file: " + e.Message);
//                }

//                return new AccountListDto
//                {
//                    OffChainID = (long)OutputDTO.ReturnValue1,
//                    Address = OutputDTO.ReturnValue2,
//                    Name = OutputDTO.ReturnValue3,
//                    LastName = OutputDTO.ReturnValue4,
//                    Email = OutputDTO.ReturnValue5,
//                    Date = UnixTimeToDateTime(OutputDTO.ReturnValue6),
//                    Keyfile = keyStore,
//                };
//            }
//            catch (Exception exception)
//            {
//                Console.WriteLine(exception.Message);
//                return new AccountListDto();
//            }
//        }
//        private async Task<long> QueryAccountsAsync()
//        {
//            var query = await _contractHandler.QueryAsync<AccountsCountFunction, BigInteger>();
//            return (long)query;
                                               
            
//        }
//        private byte[] StringToByte(string data)
//        {
//            return Encoding.ASCII.GetBytes(data);
//        }
//        private DateTime UnixTimeToDateTime(BigInteger unixtime)
//        {
//            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
//            dtDateTime = dtDateTime.AddSeconds((long)unixtime).ToLocalTime();
//            return dtDateTime;
//        }

//    }
//}
