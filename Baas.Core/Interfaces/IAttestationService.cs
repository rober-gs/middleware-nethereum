using System.Collections.Generic;
using System.Threading.Tasks;
using Baas.Core.DTOs;
using Nethereum.RPC.Eth.DTOs;

namespace Baas.Core.Interfaces
{
    public interface IAttestationService
    {
        Task<AttestationResponse> AddAsync(AddAttestationDTO add);
        Task<AttestDto> GetByIndexAsync(int id);
        Task<List<AttestDto>> GetAsync(string filter = null);        
        Task<List<AttestDto>> GetByOwnerAsync(string userDicio);
        Task<string> CountQueryAsync();
    }
}