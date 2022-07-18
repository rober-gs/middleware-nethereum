using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Baas.Core.DTOs;
using Baas.Core.Exceptions;
using Nethereum.ABI.Decoders;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;
using static Baas.Core.BlockchainDtos.SearchFunctions;

namespace Baas.Core.Services
{
    public class SearchService
    {        
        private readonly Web3Service _contract;
        private readonly Account _account;

        private readonly string SMARTCONTRACT_ADDRESS_SEARCH_CURP;

        public SearchService(VaultSignerDTO account)
        {
            SMARTCONTRACT_ADDRESS_SEARCH_CURP = Environment.GetEnvironmentVariable("SMARTCONTRACT_ADDRESS_SEARCH_CURP");
            _account = new Account(account.PrivateKey, Chain.Rinkeby);
            _contract = new Web3Service(SMARTCONTRACT_ADDRESS_SEARCH_CURP, _account);
        }
        public async Task<BlockchainData> SearchCurpAsync(string uuid, string cid, string curp)
        {
            try
            {
                CreateSearchFunction createSearchFunction = new CreateSearchFunction
                {
                    Uuid = uuid,
                    Cid = cid,
                    Curp = curp                    
                };

                TransactionReceipt txnReceipt = await _contract.Handler.SendRequestAndWaitForReceiptAsync(createSearchFunction, null);

                BlockchainData blockchainData = new BlockchainData
                {
                    TransactionHash = txnReceipt.TransactionHash,
                    BlockHash = txnReceipt.BlockHash,
                    BlockNumber = (long)txnReceipt.BlockNumber.Value,
                    GasUsed = txnReceipt.GasUsed.Value.ToString(),
                    Status = (long)txnReceipt.Status.Value
                };

                blockchainData.TransactionHash = txnReceipt.TransactionHash;                

                return blockchainData;

            }
            catch (Exception ex)
            {
                throw new CustomException("Algo a pasado coño "+  ex.Message);
            }
        }
        public async Task<BlockchainData> ParticipateCurpAsync(string uuid, string cid)
        {
            try
            {
                ParticipateFunction participate = new ParticipateFunction
                {
                    Uuid = uuid,
                    Cid = cid,
                    
                };

                TransactionReceipt txnReceipt = await _contract.Handler.SendRequestAndWaitForReceiptAsync(participate);

                BlockchainData blockchainData = new BlockchainData
                {
                    TransactionHash = txnReceipt.TransactionHash,
                    BlockHash = txnReceipt.BlockHash,
                    BlockNumber = (long)txnReceipt.BlockNumber.Value,
                    GasUsed = txnReceipt.GasUsed.Value.ToString(),
                    Status = (long)txnReceipt.Status.Value
                };

                blockchainData.TransactionHash = txnReceipt.TransactionHash;

                return blockchainData;
            }

            catch (Exception ex)
            {
                throw new CustomException("Algo a pasado coño " + ex.Message);
            }
        }
        public async Task<int> GetQuorumAsync()
        {
            try
            {                
                BigInteger data = await _contract.Handler.QueryAsync<QuorumFunction, BigInteger>();
                return (int) data;
            }

            catch (Exception ex)
            {
                throw new CustomException("Algo a pasado coño " + ex.Message);
            }
        }
        public async Task<BlockchainData> SetQuorumAsync(int quorum)
        {
            try
            {
                ChangeQuorumFunction function = new ChangeQuorumFunction
                {
                    Quorum = quorum
                };

                TransactionReceipt txnReceipt = await _contract.Handler.SendRequestAndWaitForReceiptAsync(function);

                BlockchainData blockchainData = new BlockchainData
                {
                    TransactionHash = txnReceipt.TransactionHash,
                    BlockHash = txnReceipt.BlockHash,
                    BlockNumber = (long)txnReceipt.BlockNumber.Value,
                    GasUsed = txnReceipt.GasUsed.Value.ToString(),
                    Status = (long)txnReceipt.Status.Value
                };

                blockchainData.TransactionHash = txnReceipt.TransactionHash;

                return blockchainData;
            }

            catch (Exception ex)
            {
                throw new CustomException("Algo a pasado coño " + ex.Message);
            }
        }
        public async Task<DetailSearchOutputDTO> SearchExist(string uuid)
        {
            try
            {
                DetailSearchFunction function = new DetailSearchFunction
                {
                    Uuid = uuid
                };

                DetailSearchOutputDTO data = await _contract.Handler.QueryDeserializingToObjectAsync<DetailSearchFunction, DetailSearchOutputDTO>(function);

                return data;
            }

            catch (Exception ex)
            {
                throw new CustomException("Algo a pasado coño " + ex.Message);
            }
        }
        public async Task<SearchDetailDTO> DetailSearch(string uuid)
        {
            try
            {                
                DetailSearchOutputDTO data = await SearchExist(uuid);
                BroadcastDTO dataMapping = new BroadcastDTO
                {
                    Owner = data.Owner,
                    Uuid = data.Uuid,
                    Cid = data.Cid,
                    //TODO Quitar la URL en duro y pasarla por environtment  
                    CidUri = new Uri(string.Concat("https://infura-ipfs.io/ipfs/", data.Cid)),
                    Curp = data.Curp,
                    CreateBroadcast = UnixTimeToDateTime(data.CreateBroadcast),
                    EndBroadcast =UnixTimeToDateTime(data.EndBroadcast),
                    //TODO realizar enum
                    State = data.State,
                    Records = (int)data.Records,
                    EndDate = UnixTimeToDateTime(data.EndDate)
                };
         

                List<ParticipationDTO> records = new List<ParticipationDTO>();

                if (data.Records > 0) 
                {
                    for (int i = 1; i <= data.Records; i++)
                    {
                        records.Add(await DetailParticipation(uuid, i));
                    }
                }

                SearchDetailDTO result = new SearchDetailDTO
                {
                    Search = dataMapping,
                    Records = records
                };

                return result;
            }

            catch (Exception ex)
            {
                throw new CustomException("Algo a pasado coño " + ex.Message);
            }
        }        
        public async Task<ParticipationDTO> DetailParticipation(string uuid, int id)
        {
            try
            {
                DetailParticipationFunction function = new DetailParticipationFunction
                {
                    Uuid = uuid,
                    Id = id
                };

                DetailParticipationOutputDTO data = await _contract.Handler.QueryDeserializingToObjectAsync<DetailParticipationFunction, DetailParticipationOutputDTO>(function);

                ParticipationDTO dataMappig = new ParticipationDTO
                {
                    Owner = data.Participant,
                    //TODO Quitar la URL en duro y pasarla por environtment  
                    CidUri = new Uri(string.Concat("https://infura-ipfs.io/ipfs/", data.Cid)),
                    Cid = data.Cid,
                    CreateDate = UnixTimeToDateTime(data.RecordDate)
                };

                return dataMappig;

            }
            catch (Exception ex)
            {
                throw new CustomException("Algo a pasado coño " + ex.Message);
            }
        }      
        public async Task<BlockchainData> ScoreSearchAsync(string uuid, string cid)
        {
            try
            {
                SetScoreFunction function = new SetScoreFunction
                {
                    Uuid = uuid,
                    Cid = cid
                };

                TransactionReceipt txnReceipt = await _contract.Handler.SendRequestAndWaitForReceiptAsync(function);               

                BlockchainData blockchainData = new BlockchainData
                {
                    TransactionHash = txnReceipt.TransactionHash,
                    BlockHash = txnReceipt.BlockHash,
                    BlockNumber = (long)txnReceipt.BlockNumber.Value,
                    GasUsed = txnReceipt.GasUsed.Value.ToString(),
                    Status = (long)txnReceipt.Status.Value
                };

                blockchainData.TransactionHash = txnReceipt.TransactionHash;                

                return blockchainData;
                
            }
            catch (Exception ex)
            {
                throw new CustomException("Algo a pasado coño " + ex.Message);
            }
        }
        private DateTime UnixTimeToDateTime(BigInteger unixtime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            if (unixtime == 0) return dtDateTime;

            dtDateTime = dtDateTime.AddSeconds((long)unixtime).ToLocalTime();
            return dtDateTime;
        }
    }
}
