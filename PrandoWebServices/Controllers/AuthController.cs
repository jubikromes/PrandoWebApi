using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrandoWebServices.Services.UserService;
using PrandoWebServices.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrandoWebServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAccountService _accountSvc;
        public AuthController(IAccountService accountSvc)
        {
            _accountSvc = accountSvc;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            var response = new JsonWrapperResponse<TokenResponseViewModel> { };
            if (!ModelState.IsValid)
            {
                response.ResponseCode = StatusCodes.Status400BadRequest;
                response.Errors = ModelState.FirstOrDefault().Value?.Errors?.Select(p => p.ErrorMessage).ToList();
                return Json(response);
            }
            try
            {
                response = await _accountSvc.Login(credentials);
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.HasError = true;
                response.ResponseCode = StatusCodes.Status500InternalServerError;
                return Json(response);
            }
            return Json(response);
        }

    }
}