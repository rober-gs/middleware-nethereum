using System;
using System.Threading.Tasks;
using Baas.Core.DTOs;
using Microsoft.AspNetCore.Http;

namespace Baas.Core.Interfaces
{
	public interface IIpfsService
	{
		Task<IpfsState> GetStateAsync();
        Task<DirectoryList> ListAsync(string hashOrPath);
        Task<Entry> FileWrite(string path, IFormFile file);
		Task<string> FileCat(string cid);
	}
}

