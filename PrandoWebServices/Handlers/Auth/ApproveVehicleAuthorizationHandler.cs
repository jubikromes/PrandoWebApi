using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PrandoWebServices.Data.Entities;
using PrandoWebServices.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrandoWebServices.Filters.Auth
{
    public class ApproveVehicleAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
    {
        public ApproveVehicleAuthorizationHandler()
        {

        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
        {
            if (context.User == null)
                return Task.CompletedTask;
            return Task.CompletedTask;
        }
    }
}
