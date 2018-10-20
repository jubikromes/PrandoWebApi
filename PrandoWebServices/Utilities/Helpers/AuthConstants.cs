using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PrandoWebServices.Utilities.Helpers.Constants.Strings;

namespace PrandoWebServices.Utilities.Helpers
{
    public class AuthorizationConstants
    {
        public static class PrandoOperations
        {
            public static OperationAuthorizationRequirement ApproveVehicle = new OperationAuthorizationRequirement {
                Name = JwtClaims.Pol_ApproveVehicle
            };
        }
    }
}
