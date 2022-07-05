using System.Threading.Tasks;
using Baas.Core.DTOs;
using Microsoft.AspNetCore.Http;

namespace Baas.Core.Interfaces
{
    public interface ICurpService
    {
        Task<SearchDetailDTO> DetailSearchAsync(string uuid);
        Task<ParticipationDTO> DetailRecordsAsync(string uuid, int id);

        Task<ResponseCurpDTO> AddSearchAsync(string curp, string uuid, IFormFile file);
        Task<ResponseCurpDTO> AddRecordAsync(string curp, string uuid, IFormFile file);

        Task<BlockchainData> IncrementQuorumAsync();
        Task<BlockchainData> DecrementQuorumAsync();
        Task<int> GetCurrentQuorumAsync();
    }
}
