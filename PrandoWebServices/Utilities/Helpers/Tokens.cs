using Newtonsoft.Json;
using PrandoWebServices.Auth.Models;
using PrandoWebServices.Auth.TokenFactory;
using PrandoWebServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrandoWebServices.Utilities.Helpers
{
    public class Tokens
    {
        public static async Task<TokenResponseViewModel> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new TokenResponseViewModel
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };
            //return JsonConvert.SerializeObject(response, serializerSettings);
            return response;
        }
    }
}
