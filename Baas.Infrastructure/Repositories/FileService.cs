using Baas.Core.Exceptions;
using Baas.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Baas.Infrastructure
{
    public class FileService : IFileService
    {
        private readonly string _fileServer;

        public FileService(string fileServer)
        {
            _fileServer = fileServer;
        }

        public FileService()
        {
            _fileServer = Directory.GetCurrentDirectory();
        }

        public async Task<bool> SaveFileAsync(string path, string json)
        {
            try
            {
                string fullPath = Path.Combine(_fileServer, path);
                string directoryName = Path.GetDirectoryName(fullPath);

                if (!File.Exists(directoryName)) Directory.CreateDirectory(directoryName);

                using (StreamWriter streamWriter = new StreamWriter(fullPath))
                {
                    await streamWriter.WriteAsync(json);
                }

                return true;
            }
            catch (Exception exception)
            {
                throw new CustomException(exception.Message);
            }
        }
        public async Task<string> ReadFileAsync(string path)
        {
            try
            {
                string json = string.Empty;
                string fullPath = Path.Combine(_fileServer, path);
                string directoryName = Path.GetDirectoryName(fullPath);

                using (StreamReader streamReader = new StreamReader(Path.GetDirectoryName(fullPath)))
                {
                    json = await streamReader.ReadToEndAsync();
                }

                return json;
            }
            catch (Exception exception)
            {
                throw new CustomException(exception.Message);
            }
        }
    }
}
