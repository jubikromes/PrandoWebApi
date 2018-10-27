using AutoMapper;
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
    public class AccountsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountSvc;
        public AccountsController(IMapper mapper, IAccountService accountSvct)
        {
            _mapper = mapper;
            _accountSvc = accountSvct;
        }
        [Route("register")]
        public async Task<IActionResult> Post([FromBody] RegistrationViewModel model)
        {
            var response = new JsonResponse { };
            if (!ModelState.IsValid)
            {
                response.ResponseCode = StatusCodes.Status400BadRequest;
                response.Errors = ModelState.FirstOrDefault().Value?.Errors?.Select(p => p.ErrorMessage).ToList();
                return BadRequest(response);
            }
            try
            {
                response = await _accountSvc.Register(model);
            }
            catch (Exception ex)
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