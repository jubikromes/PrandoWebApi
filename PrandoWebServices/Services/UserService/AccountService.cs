using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PrandoWebServices.Auth.Models;
using PrandoWebServices.Auth.TokenFactory;
using PrandoWebServices.Data.Entities;
using PrandoWebServices.Data.Identity;
using PrandoWebServices.DbContext;
using PrandoWebServices.Repositories;
using PrandoWebServices.Utilities.Helpers;
using PrandoWebServices.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrandoWebServices.Services.UserService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        //use repository instead
        private readonly PrandoDbContext _prandoDbContext;

        private readonly IRepository<Owner> _ownerRepository;

        public AccountService(UserManager<AppUser> userManager,
                    IJwtFactory jwtFactory, 
                    IOptions<JwtIssuerOptions> jwtOptions,
                    PrandoDbContext prandoDbContext,
                    IRepository<Owner> ownerRepository)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _prandoDbContext = prandoDbContext;
            _ownerRepository = ownerRepository;
        }
        public async Task<JsonWrapperResponse<TokenResponseViewModel>> Login(CredentialsViewModel credentials)
        {
            var response = new JsonWrapperResponse<TokenResponseViewModel> { };
            var identity = await GetClaims(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                response.Message = "Could not generate token";
                response.HasError = true;
                return response;
            }
            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            response.Result = jwt;
            response.HasError = false;
            response.ResponseCode = StatusCodes.Status200OK;
            return response;
        }
        public async Task<JsonResponse> Register(RegistrationViewModel model)
        {
            var response = new JsonResponse { };
            var user = new AppUser
            {
                LastName = model.LastName,
                Email = model.Email,
                FirstName = model.FirstName,
                UserName = model.UserName
            };
            var identityResult = await _userManager.CreateAsync(user, model.Password);
            if (!identityResult.Succeeded)
            {
                response.Message = "Could not register user.";
                response.HasError = true;
                return response;
            }
            //use repository instead 
            await _prandoDbContext.AddAsync(new Owner { Gender = 'M', IdentityUserId = user.Id });
            await _prandoDbContext.SaveChangesAsync();
            //use repository instead 
            await _ownerRepository.Add(new Owner { Gender = 'M', IdentityUserId = user.Id });
            response.Message = "Successfully registered";
            response.HasError = false;
            response.ResponseCode = StatusCodes.Status200OK;
            return response;
        }
        private async Task<ClaimsIdentity> GetClaims(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);
            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(username);
            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(username, userToVerify.Id));
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
