using Baas.Core.DTOs;

namespace Baas.Core.Interfaces
{
    public interface IVaultServices
    {
        VaultSignerDTO GetSigner();
    }
}