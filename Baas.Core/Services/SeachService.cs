using System;
using System.Numerics;
using System.Threading.Tasks;
using Baas.Core.DTOs;
using Baas.Core.Exceptions;
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
    }
}
