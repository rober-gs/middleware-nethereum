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


        public async Task<ResponseCurpDTO> AddSearchAsync(string curp, string uuid, IFormFile file)
        {
            try
            {
                /*
                 *  Vault 
                */
                VaultSignerDTO signer = _vault.GetSigner();
                //VaultSignerDTO signer = new VaultSignerDTO
                //{
                //    PublicKey = "0x08bC5fB910C00253B0bc49a9B2a3B177E08a7877",
                //    PrivateKey = "0x0a5dc89207866cdf0f32852f2be845640369cf911490983c870470b6aff2cd8a",
                //    Mnemonic = "algo"
                //};

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

        public async Task<ResponseCurpDTO> InfoSearchAsync(string uuid)
        {
            try
            {
                ResponseCurpDTO result = new ResponseCurpDTO();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustomException(ex.Message);
            }

        }

        public async Task<ResponseCurpDTO> ParticipateAsync(string curp, string uuid, IFormFile file)
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
                BlockchainData blockchain = await contract.SearchCurpAsync(uuid, cid, curp);


                ResponseCurpDTO result = new ResponseCurpDTO
                {
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

        public async Task<ResponseCurpDTO> ChangeQuorumAsync(int quorum)
        {
            try
            {
                /*
                 *  Vault 
                */
                VaultSignerDTO signer = _vault.GetSigner();                

                /*
                 *  Blockchain
                */
                SearchService contract = new SearchService(signer);
                BlockchainData blockchain = await contract.SetQuorumAsync(quorum);


                ResponseCurpDTO result = new ResponseCurpDTO
                {                    
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

    }
}
