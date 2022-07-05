using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Baas.Core.DTOs;
using Baas.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Middelware.Controllers
{
    [Produces("application/json")]
    [Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<CurpController> _logger;
        private readonly ICurpService _curp;

        public SettingsController(ILogger<CurpController> logger, ICurpService curp)
        {
            _logger = logger;
            _curp = curp;
        }

        [HttpGet("quorum")]
        public async Task<ApiResponse<int>> GetQuorum()
        {
            try
            {  
                _logger.LogInformation("Get Quorum ");

                int data = await _curp.GetCurrentQuorumAsync();
                var response = new ApiResponse<int>(data);
                return response;

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw new Exception(exception.Message);
            }
        }
        [HttpPost("quorumIncrement")]
        public async Task<ActionResult> IncrementQuorum()
        {
            try
            {  
                _logger.LogInformation("++ Quorum ");

                var data = await _curp.IncrementQuorumAsync();
                
                return new CreatedResult(nameof(GetQuorum), new {data});

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw new Exception(exception.Message);
            }
        }
        [HttpPost("quorumDecrement")]
        public async Task<ActionResult> DecrementQuorum()
        {
            try
            {  
                _logger.LogInformation("-- Quorum ");

                var data = await _curp.DecrementQuorumAsync();
                
                return new CreatedResult(nameof(GetQuorum), new {data});

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw new Exception(exception.Message);
            }
        }
       

    }




}

