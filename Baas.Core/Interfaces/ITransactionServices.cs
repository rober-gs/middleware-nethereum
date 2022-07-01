using Baas.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Baas.Core.Interfaces
{
    public interface ITransactionServices
    {
        Task<long> GetLastBlockAsync();
        Task<List<TrasactionDTO>> GetTransactionAsync();
    }
}