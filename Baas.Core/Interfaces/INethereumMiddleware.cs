using Nethereum.Web3.Accounts;
using Nethereum.Web3;

namespace Baas.Core.Interfaces
{
    public interface INethereumMiddleware
    {
        Web3 ConnectNodo();
        Web3 ConnectNodo(Account account);
        Web3 ConnectNodo(Account account, string Url);
    }
}