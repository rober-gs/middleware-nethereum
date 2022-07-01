using System.Threading.Tasks;
using Baas.Core.DTOs;
using Microsoft.AspNetCore.Http;

namespace Baas.Core.Interfaces
{
    public interface ICurpService
    {
        Task<ResponseCurpDTO> AddSearchAsync(string curp, string uuid, IFormFile file);
        Task<ResponseCurpDTO> InfoSearchAsync(string uuid);
        Task<ResponseCurpDTO> ParticipateAsync(string curp, string uuid, IFormFile file);
        Task<ResponseCurpDTO> ChangeQuorumAsync(int quorum);
    }
}
