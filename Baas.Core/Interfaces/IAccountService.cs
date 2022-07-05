using System.Threading.Tasks;
using Baas.Core.DTOs;

namespace Baas.Core.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDTO> AccountInfoAsync();
    }
}