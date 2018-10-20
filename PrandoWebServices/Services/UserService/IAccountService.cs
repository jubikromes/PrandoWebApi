using PrandoWebServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrandoWebServices.Services.UserService
{
    public interface IAccountService 
    {
        Task<JsonWrapperResponse<TokenResponseViewModel>> Login(CredentialsViewModel model);

        Task<JsonResponse> Register(RegistrationViewModel model);
    }
}
