using System.Threading.Tasks;

namespace Baas.Core.Interfaces
{
    public interface IFileService
    {
        Task<string> ReadFileAsync(string path);
        Task<bool> SaveFileAsync(string path, string json);
    }
}