using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Baas.Core.DTOs;
using Baas.Core.Exceptions;
using Baas.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;

namespace Baas.Core.Services
{
    public class IpfsService : IIpfsService
    {
        private readonly string ROOT = "%2F";
        private readonly string _baseUri;

        public IpfsService()
        {
            string uri = Environment.GetEnvironmentVariable("IPFS_SERVER_BASE_URL");
            string port = Environment.GetEnvironmentVariable("IPFS_SERVER_PORT");
            string apiVersion = Environment.GetEnvironmentVariable("IPFS_API_VERSION");

            StringBuilder baseUri = new StringBuilder();

            _baseUri = baseUri
                .Append(uri)
                .Append(port)
                .Append(apiVersion)
                .ToString();
        }

        public async Task<IpfsState> GetStateAsync()
        {
            IpfsState state = new IpfsState();

            try
            {
                state.Identity = await IpfsIDAsync();
                state.Root = await IpfsFileStatAsync(ROOT);
                state.BandWidth = await IpfsBandWidthAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception("IpfsServices", ex);
            }

            return state;
        }
        public async Task<DirectoryList> ListAsync(string hashOrPath = "%2F")
        {
            DirectoryList data = new DirectoryList();

            try
            {                
                data = await IpfsFilesLsAsync(hashOrPath);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return data;
        }
        public async Task<string> FileCat(string cid)
        {
            try
            {
                string resource = string.Concat("cat?arg=", cid);
                Method htmlMethod = Method.Post;
                int timeOut = 5000;

                RestClient cliente = new RestClient(_baseUri);
                RestRequest request = new RestRequest(resource, htmlMethod);
                request.AddHeader("Authorization", "Basic MjcxWWY4Z1VEQlZ4bWNsdFpsQVFoVlhXT1ZUOjNkYzdjOTQ4YmM5NjhhNWIyNjE2Zjc5NGM4MDNkZTQ3");
               
                request.Timeout = timeOut;

                RestResponse response = await cliente.ExecutePostAsync(request);

                if (response.ErrorException != null) throw new CustomException(response.ErrorException.Message);
                if (!response.IsSuccessful) throw new CustomException("ALGOI FALLO IPFS" + response);

                return response.Content;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }            
        }

        public async Task<Entry> FileWrite(string path, IFormFile file)
        {            

            try
            {
                Entry data = new Entry();

                if (string.IsNullOrEmpty(path)) throw new CustomException("IPFS SERVICES: This Path is not Valid");

                string resource = string.Concat("add?progress=false&wrap-with-directory=false&pin=true");
                Method htmlMethod = Method.Post;
                int timeOut = 5000;

                RestClient cliente = new RestClient(_baseUri);
                RestRequest request = new RestRequest(resource, htmlMethod);
                request.AddHeader("Authorization", "Basic MjcxWWY4Z1VEQlZ4bWNsdFpsQVFoVlhXT1ZUOjNkYzdjOTQ4YmM5NjhhNWIyNjE2Zjc5NGM4MDNkZTQ3");
                request.AddFile(file.Name, file.OpenReadStream, file.FileName);            

                request.Timeout = timeOut;

                RestResponse response = await cliente.ExecutePostAsync(request);

                if (response.ErrorException != null) throw new CustomException(response.ErrorException.Message);
                if (!response.IsSuccessful) throw new CustomException("NO PUDE METERLO AL IPFS" + response);

                    data = JsonConvert.DeserializeObject<Entry>(response.Content);
                    //TODO ver opcion de un constrructor para que se genere en automatico
                    data.CidUri = new Uri(string.Concat("https://infura-ipfs.io/ipfs/", data.Hash));

                /*
                 * resource = string.Concat("files/cp?", "arg=%2Fipfs%2F", data.Hash, "&arg=", path, "&parents=true");                    

                RestClient client = new RestClient(_baseUri);
                RestRequest requestCP = new RestRequest(resource, htmlMethod);
                requestCP.AddHeader("Authorization", "Basic MjcxWWY4Z1VEQlZ4bWNsdFpsQVFoVlhXT1ZUOjNkYzdjOTQ4YmM5NjhhNWIyNjE2Zjc5NGM4MDNkZTQ3");

                RestResponse responseCP = await client.ExecutePostAsync(requestCP);

                if (responseCP.ErrorException != null) throw new CustomException(responseCP.ErrorException.Message);
                if (!responseCP.IsSuccessful) throw new CustomException(responseCP.Content);
                 */

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }            
        }





        private async Task<IpfsID> IpfsIDAsync()
        {
            IpfsID ipfsID = new IpfsID();

            try
            {
                string resource = "/id";
                Method htmlMethod = Method.Post;
                int timeOut = 5000;

                RestClient cliente = new RestClient(_baseUri);
                RestRequest request = new RestRequest(resource, htmlMethod);
                request.Timeout = timeOut;

                RestResponse response = await cliente.ExecutePostAsync(request);

                if (response.ErrorException != null) throw new CustomException(response.ErrorException.Message);
                if (response.IsSuccessful)
                {
                    ipfsID = JsonConvert.DeserializeObject<IpfsID>(response.Content);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return ipfsID;
        }
        private async Task<BandWidth> IpfsBandWidthAsync()
        {
            BandWidth ipfsBW = new BandWidth();

            try
            {
                string resource = "/stats/bw";
                Method htmlMethod = Method.Post;
                int timeOut = 5000;

                RestClient cliente = new RestClient(_baseUri);
                RestRequest request = new RestRequest(resource, htmlMethod);
                request.Timeout = timeOut;

                RestResponse response = await cliente.ExecutePostAsync(request);

                if (response.ErrorException != null) throw new CustomException(response.ErrorException.Message);

                if (response.IsSuccessful)
                {
                    ipfsBW = JsonConvert.DeserializeObject<BandWidth>(response.Content);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return ipfsBW;
        }
        private async Task<FileStat> IpfsFileStatAsync(string path)
        {
            FileStat ipfsStat = new FileStat();

            try
            {
                string resource = string.Concat("files/stat?arg=", path);
                Method htmlMethod = Method.Post;
                int timeOut = 5000;

                RestClient cliente = new RestClient(_baseUri);
                RestRequest request = new RestRequest(resource, htmlMethod);
                request.Timeout = timeOut;

                RestResponse response = await cliente.ExecutePostAsync(request);

                if (response.ErrorException != null) throw new CustomException(response.ErrorException.Message);
                if (response.IsSuccessful)
                {
                    ipfsStat = JsonConvert.DeserializeObject<FileStat>(response.Content);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return ipfsStat;
        }
        private async Task<DirectoryList> IpfsFilesLsAsync(string path)
        {
            DirectoryList data = new DirectoryList();

            try
            {
                string resource = string.Concat("files/ls?arg=", path, "&long=true");
                Method htmlMethod = Method.Post;
                int timeOut = 5000;
               
                string fileName = Path.GetFileName(path);

                RestClient cliente = new RestClient(_baseUri);
                RestRequest request = new RestRequest(resource, htmlMethod);
                request.Timeout = timeOut;

                RestResponse response = await cliente.ExecutePostAsync(request);

                if (response.ErrorException != null) throw new CustomException(response.ErrorException.Message);
                if (response.IsSuccessful)
                {
                    data = JsonConvert.DeserializeObject<DirectoryList>(response.Content);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return data;
        }       
    }
}