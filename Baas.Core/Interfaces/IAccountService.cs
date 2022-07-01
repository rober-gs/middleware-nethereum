using Baas.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Baas.Core.Interfaces
{
    public interface IAccountService
    {
        AccountDTO GenerateAccount(string password);
        Task<AccountResponseDTO> AddAccountAsync(AddAccountDTO dto);
        Task<List<AccountListDto>> GetAccountList(string filter);
        Task<AccountListDto> GetAccountEthAsync(string address);
        Task<AccountListDto> GetAccountByOffchainIDAsync(int idOffchain);
    }
}