using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Baas.Core.Exceptions;
using Baas.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Middelware.Controllers
{
    [Produces("application/json")]
    [Route("api/ipfs")]
    [ApiController]
    public class IpfsController : ControllerBase
    {
        private readonly ILogger<IpfsController> _logger;
        private readonly IIpfsService _ipfsService;

        public IpfsController(ILogger<IpfsController> logger, IIpfsService ipfsService)
        {
            _logger = logger;
            _ipfsService = ipfsService;
        }

        [HttpGet("{cid}")]
        public async Task<string> Get(string cid)
        {
            try
            {
                _logger.LogInformation("Get JSON Ipfs");

                if (string.IsNullOrEmpty(cid)) throw new CustomException("cid is required");
                var data = await _ipfsService.FileCat(cid);

                return data;
            }
            catch (Exception exception)
            {
                throw new CustomException(exception.Message);
            }
        }
    }
}

