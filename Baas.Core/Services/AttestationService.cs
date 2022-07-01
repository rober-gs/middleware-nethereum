//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;
//using Baas.Core.BlockchainDtos;
//using Baas.Core.DTOs;
//using Baas.Core.Exceptions;
//using Baas.Core.Interfaces;
//using Microsoft.Extensions.Configuration;
//using Nethereum.Contracts;
//using Nethereum.Contracts.ContractHandlers;
//using Nethereum.RPC.Eth.DTOs;
//using Nethereum.Web3;


//namespace Baas.Core.Services
//{
//    public class AttestationService : IAttestationService
//    {
//        private readonly INethereumMiddleware _nethereum;
//        private readonly IConfiguration _configuration;
//        private readonly ContractHandler _contractHandler;
//        private readonly string _smartContract;
//        private readonly string DICIO_PUBLIC_KEY = "0x29410986f3010788A3d757edcEf242CBd7bC6ce6";

//        public AttestationService(INethereumMiddleware nethereum, IConfiguration configuration)
//        {
//            _nethereum = nethereum;
//            _configuration = configuration;
//            _smartContract = _configuration.GetSection("ContractsPropities").GetValue<string>("ADDRESS_CONTRACT_ATTESTATION");      //"0xBde7F538E1f6B2b29740d938292b46e801a3F742";    //address contract rinkeby  310521   
//            _contractHandler = _nethereum.ConnectNodo().Eth.GetContractHandler(_smartContract);
//        }

//        public async Task<AttestationResponse> AddAsync(AddAttestationDTO add)
//        {

//            if (add.Account != string.Empty && !Web3.IsChecksumAddress(add.Account)) throw new CustomException("This Account is not valid");
//            if (string.IsNullOrEmpty(add.IdOffchain) || string.IsNullOrWhiteSpace(add.IdOffchain)) throw new CustomException("This id is not valid, cannot be null");
//            if (string.IsNullOrEmpty(add.Value) || string.IsNullOrWhiteSpace(add.Value)) throw new CustomException("The evidence value cannot be null");
            
//            AttestationResponse data = new AttestationResponse();


//            var attestationFunction = new AddAttestationFunction 
//            {
//                Value = add.Value,
//                Id = add.IdOffchain,
//                Status =   Convert.ToByte(add.Status),
//                UserEth = string.IsNullOrEmpty(add.Account) ? DICIO_PUBLIC_KEY : add.Account,
//                TimeStamp = ((long)add.TimeStamp.Subtract( new DateTime(1970, 1, 1)).TotalSeconds),
//            };

//            try
//            {
//                TransactionReceipt tx = await _contractHandler.SendRequestAndWaitForReceiptAsync( attestationFunction, null);
//                var newEvent = tx.DecodeAllEvents<NewAttestationEventDTO>()[0].Event;
//                var attestation = new AttestationData
//                {
//                    IndexAttestation = (long)newEvent.Index,
//                    AccountAttestation = newEvent.User,
//                    IdOffchain = newEvent.Id,
//                    Value = newEvent.Value,
//                    TimeStamp = UnixTimeToDateTime(newEvent.Timestamp),
//                    Status = newEvent.Status,
//                    CreateDate = UnixTimeToDateTime(newEvent.CreateDate),
//                };

//                var blockChain = new BlockchainData
//                {
//                    TransactionHash = tx.TransactionHash,
//                    GasUsed = tx.GasUsed.ToString(),
//                    BlockNumber = (long)tx.BlockNumber.Value,
//                    BlockHash = tx.BlockHash,
//                    //Status = bool.Parse(tx.Status.Value.ToString())
//                };

//                data.Attestation = attestation;
//                data.Blockchain = blockChain;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex);
//            } 
//            return  data;
//        }

//        public async Task<List<AttestDto>> GetByOwnerAsync(string account)
//        {
//            if (account != string.Empty && !Web3.IsChecksumAddress(Web3.ToChecksumAddress(account))) throw new CustomException("This account is not valid");

//            List<AttestDto> data = new List<AttestDto>();

//            var getAttestationsByOwnerFunction = new GetAttestationsByUserFunction
//            {
//                UserEth = account
//            };

//            var listID = await _contractHandler.QueryAsync<GetAttestationsByUserFunction, List<BigInteger>>(getAttestationsByOwnerFunction, null);

//            foreach (var id in listID)
//            {
//                data.Add(await GetByIdAsync(id)); //;
//            }

//            return data;
//        }

//        public async Task<AttestDto> GetByIndexAsync(int id)
//        {
//            return await GetByIdAsync(id);             
//        }

//        public async Task<List<AttestDto>> GetAsync(string filter = null)
//        {
//            List<AttestDto> data = new List<AttestDto>();

//            try
//            {
//                var count = await _contractHandler.QueryAsync<AttestationsCountFunction, BigInteger>(null, null);
//                if (count > 0)
//                {
//                    for (int i = 0; i <= count - 1; i++)
//                    {
//                        data.Add(await GetByIdAsync(i) ?? null);
//                    }
//                }

//                if (!string.IsNullOrWhiteSpace(filter) || !string.IsNullOrEmpty(filter))
//                {
//                    List<AttestDto> dataFilter = data.Where(x => x.IdOffchain.ToString() == filter).ToList();
//                    return dataFilter;
//                }

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);                
//            }

//            return data;

//        }

//        public async Task<string> CountQueryAsync()
//        {
//            var attestationsCount = await _contractHandler.QueryAsync<AttestationsCountFunction, BigInteger>(null, null);
//            return attestationsCount.ToString();
//        }

//        private async Task<AttestDto> GetByIdAsync(BigInteger idAtestation, BlockParameter blockParameter = null)
//        {
//            AttestDto data = new AttestDto();

//            try
//            {
//                GetAttestationsByIndexFunction getAttestationsByIdFunction = new GetAttestationsByIndexFunction
//                {
//                    IndexAttestation = idAtestation
//                };

//                GetAttestationsByIndexOutputDTO result = await _contractHandler.QueryDeserializingToObjectAsync<GetAttestationsByIndexFunction, GetAttestationsByIndexOutputDTO>(getAttestationsByIdFunction, blockParameter);


//                data.IdOffchain = result.ReturnValue1;
//                data.Value = result.ReturnValue2;
//                data.TimeStamp = UnixTimeToDateTime(result.ReturnValue3);
//                data.CreateDate = UnixTimeToDateTime(result.ReturnValue4);
//                data.Estatus = (int)result.ReturnValue5;
                
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }

//            return data;
//        }

//        private string ByteToString(byte[] data)
//        {
//            return Encoding.UTF8.GetString(data).Replace("\u0000", string.Empty);
//        }
//        private byte[] StringToByte(string data)
//        {
//            return Encoding.ASCII.GetBytes(data);
//        }
//        private DateTime UnixTimeToDateTime(BigInteger unixtime)
//        {
//            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
//            dtDateTime = dtDateTime.AddSeconds((long)unixtime).ToLocalTime();
//            return dtDateTime;
//        }
        
//    }
//}
