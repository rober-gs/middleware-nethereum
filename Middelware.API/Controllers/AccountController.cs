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
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _account;

        public AccountController(ILogger<AccountController> logger, IAccountService account)
        {
            _logger = logger;
            _account = account;
        }
        [HttpGet]
        public async Task<ApiResponse<AccountDTO>> Account()
        {
            try
            {
                _logger.LogInformation("Account Information");

                var data = await _account.AccountInfoAsync();
                var response = new ApiResponse<AccountDTO>(data);

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

