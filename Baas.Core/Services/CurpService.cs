using System;
using System.Text;
using System.Threading.Tasks;
using Baas.Core.DTOs;
using Baas.Core.Exceptions;
using Baas.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using static Baas.Core.BlockchainDtos.SearchFunctions;

namespace Baas.Core.Services
{
    public class CurpService : ICurpService
    {        
        private readonly IVaultServices _vault;        
        private readonly IIpfsService _ipfs;

        private readonly string ADDRESS_ZERO = "0x0000000000000000000000000000000000000000";

        public CurpService(IVaultServices vault, IIpfsService ipfs)
        {
            _vault = vault;
            _ipfs = ipfs;
        }

        public async Task<SearchDetailDTO> DetailSearchAsync(string uuid)
        {
            try
            {
                /*
                *
                */
                VaultSignerDTO signer = _vault.GetSigner();
                SearchService contract = new SearchService(signer);
                SearchDetailDTO search = await contract.DetailSearch(uuid);
                return search;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public async Task<ParticipationDTO> DetailRecordsAsync(string uuid, int id)
        {
            try
            {
                /*
                *
                */
                VaultSignerDTO signer = _vault.GetSigner();
                SearchService contract = new SearchService(signer);
                ParticipationDTO search = await contract.DetailParticipation(uuid, id);
                return search;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseCurpDTO> AddSearchAsync(string curp, string uuid, IFormFile file)
        {
            try
            {
                /*
                 *  Vault 
                */
                VaultSignerDTO signer = _vault.GetSigner();
                
                /*
                 * Blockchain consulta
                */
                SearchService contract = new SearchService(signer);
                DetailSearchOutputDTO search = await contract.SearchExist(uuid);

                if (search.Owner != ADDRESS_ZERO) throw new CustomException("Esta busqueda ya existe");


                /*
                 *  IPFS  
                */
                StringBuilder sbPath = new StringBuilder();
                string pathComplete = sbPath.AppendFormat("%2F{0}%2F{1}%2F{2}%2F{3}",
                       uuid,
                       curp,
                       signer.PublicKey,
                       file.FileName).ToString();

                Entry IpfsHash = await _ipfs.FileWrite(pathComplete, file);
                string cid = IpfsHash.Hash;

                /*
                 * Blockchain
                */
                BlockchainData blockchain = await contract.SearchCurpAsync(uuid, cid, curp);

                ResponseCurpDTO result = new ResponseCurpDTO
                {
                    Uuid = uuid,
                    IpfsData = IpfsHash,
                    BlockchainData = blockchain
                };

                return result;
            }

            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustomException(ex.Message);
            }
        }
        public async Task<ResponseCurpDTO> AddRecordAsync(string curp, string uuid, IFormFile file)
        {
            try
            {
                StringBuilder sbPath = new StringBuilder();

                /*
                 *  Vault 
                */
                VaultSignerDTO signer = _vault.GetSigner();

                /*
                 *  IPFS  
                */
                string pathComplete = sbPath.AppendFormat("%2F{0}%2F{1}%2F{2}_{3}",
                       curp,
                       uuid,
                       signer.PublicKey,
                       file.FileName).ToString();

                Entry IpfsHash = await _ipfs.FileWrite(pathComplete, file);
                string cid = IpfsHash.Hash;
                /*
                 *  Blockchain
                */
                SearchService contract = new SearchService(signer);
                BlockchainData blockchain = await contract.ParticipateCurpAsync(uuid, cid);


                ResponseCurpDTO result = new ResponseCurpDTO
                {
                    Uuid = uuid,
                    IpfsData = IpfsHash,
                    BlockchainData = blockchain
                };

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustomException(ex.Message);
            }

        }


        public async Task<int> GetCurrentQuorumAsync()
        {
            try
            {
                /*
                *  Vault 
                */
                VaultSignerDTO signer = _vault.GetSigner();
                
                SearchService contract = new SearchService(signer);

                int result = await contract.GetQuorumAsync();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustomException(ex.Message);
            }
        }
        public async Task<BlockchainData> IncrementQuorumAsync()
        {
            try
            {
                VaultSignerDTO signer = _vault.GetSigner();

                SearchService contract = new SearchService(signer);

                int currentQuorum = await GetCurrentQuorumAsync();

                BlockchainData result = await contract.SetQuorumAsync(currentQuorum + 1);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustomException(ex.Message);
            }
        }
        public async Task<BlockchainData> DecrementQuorumAsync()
        {
            try
            {
                VaultSignerDTO signer = _vault.GetSigner();

                SearchService contract = new SearchService(signer);

                int currentQuorum = await GetCurrentQuorumAsync();

                if (currentQuorum == 0) throw new CustomException("Ya no hay participantes, no es posible disminuir el quorum"); 

                BlockchainData result = await contract.SetQuorumAsync(currentQuorum - 1);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustomException(ex.Message);
            }
        }

    }
}
