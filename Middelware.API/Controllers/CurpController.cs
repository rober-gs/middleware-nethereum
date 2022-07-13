using Microsoft.AspNetCore.Mvc;
using Baas.Core.Exceptions;
using Baas.Core.Interfaces;
using Baas.Core.DTOs;

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
        public async Task<ApiResponse<ResponseCurpDTO>> ParticiparteAsync([FromForm] CurpRequest createdSearch)
        {
            try
            {
                string curp = createdSearch.Curp;
                string uuid = createdSearch.Uuid;
                var file = createdSearch.File;

                if (string.IsNullOrEmpty(curp)) throw new CustomException("CURP is required");
                if (string.IsNullOrEmpty(uuid)) throw new CustomException("UUID is required");
                if (file is null) throw new CustomException("File is required");
                if (file.ContentType != "application/json") throw new CustomException("File is not a json Type");

                _logger.LogInformation("Participate ");

                ResponseCurpDTO data = await _curp.AddRecordAsync(curp, uuid, createdSearch.File);
                var response = new ApiResponse<ResponseCurpDTO>(data);
                return response;

            }
            catch (Exception exception)
            {
                throw new CustomException(exception.Message);
            }
        }
        [HttpPost("score")]
        public async Task<ApiResponse<ResponseCurpDTO>> ScoreAsync([FromForm] CurpRequest createdSearch)
        {
            try
            {
                string curp = createdSearch.Curp;
                string uuid = createdSearch.Uuid;
                var file = createdSearch.File;

                if (string.IsNullOrEmpty(curp)) throw new CustomException("CURP is required");
                if (string.IsNullOrEmpty(uuid)) throw new CustomException("UUID is required");
                if (file is null) throw new CustomException("File is required");
                if (file.ContentType != "application/json") throw new CustomException("File is not a json Type");

                _logger.LogInformation("Participate ");

                ResponseCurpDTO data = await _curp.AddScoreAsync(curp, uuid, createdSearch.File);
                var response = new ApiResponse<ResponseCurpDTO>(data);
                return response;

            }
            catch (Exception exception)
            {
                throw new CustomException(exception.Message);
            }
        }
        [HttpGet("searchDetail/{uuid}")]
        public async Task<ApiResponse<SearchDetailDTO>> SearchDetail(string uuid)
        {
            try
            {
                _logger.LogInformation("Get Info Search");

                var data = await _curp.DetailSearchAsync(uuid);
                var response = new ApiResponse<SearchDetailDTO>(data);
                return response;

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw new Exception(exception.Message);
            }
        }
        [HttpGet("recordsDetail/{uuid}/{id}")]
        public async Task<ApiResponse<ParticipationDTO>> RecordsDetail(string uuid, int id)
        {
            try
            {
                _logger.LogInformation("Get info participation");

                var data = await _curp.DetailRecordsAsync(uuid, id);
                var response = new ApiResponse<ParticipationDTO>(data);
                return response;

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw new Exception(exception.Message);
            }
        }
    }
}