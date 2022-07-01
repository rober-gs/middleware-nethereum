using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baas.Core.Exceptions;
using Baas.Core.Interfaces;
using Baas.Core.DTOs;
using System.Threading.Tasks;

namespace Middelware.Controllers
{
    [Produces("application/json")]
    [Route("api/curp")]
    [ApiController]
    public class CurpController : ControllerBase
    {
        private readonly ILogger<CurpController> _logger;
        private readonly ICurpService _curp;

        public CurpController(ILogger<CurpController> logger, ICurpService curp)
        {
            _logger = logger;
            _curp = curp;
        }

        [HttpPost("search")]
        public async Task<ApiResponse<ResponseCurpDTO>> SearchAsync([FromForm] CurpRequest createdSearch)
        {
            try
            {
                string curp = createdSearch.Curp;
                string uuid = createdSearch.Uuid;

                if (string.IsNullOrEmpty(curp)) throw new CustomException("CURP is required");
                if (string.IsNullOrEmpty(uuid)) throw new CustomException("UUID is required");
                if (createdSearch.File is null) throw new CustomException("File is required");
                if (createdSearch.File.ContentType != "application/json") throw new CustomException("File is not a json Type");

                _logger.LogInformation("Search Curp! ");

                ResponseCurpDTO data = await _curp.AddSearchAsync(curp, uuid, createdSearch.File);
                var response = new ApiResponse<ResponseCurpDTO>(data);
                return response;

            }
            catch (Exception exception)
            {
                throw new CustomException(exception.Message);
            }
        }
        [HttpPost("participate")]
        public async Task<ApiResponse<ResponseCurpDTO>> Participarte([FromForm] CurpRequest createdSearch)
        {
            try
            {
                string curp = createdSearch.Curp;
                string uuid = createdSearch.Uuid;

                if (string.IsNullOrEmpty(curp)) throw new CustomException("CURP is required");
                if (string.IsNullOrEmpty(uuid)) throw new CustomException("UUID is required");
                if (createdSearch.File is null) throw new CustomException("File is required");
                if (createdSearch.File.ContentType == "application/json") throw new CustomException("File is not a json Type");

                _logger.LogInformation("Participate ");

                ResponseCurpDTO data = await _curp.AddSearchAsync(curp, uuid, createdSearch.File);
                var response = new ApiResponse<ResponseCurpDTO>(data);
                return response;

            }
            catch (Exception exception)
            {
                throw new CustomException(exception.Message);
            }
        }
    }
}