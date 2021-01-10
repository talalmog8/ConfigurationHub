using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ConfigurationHub.General
{
    public class EmptyAuthRequirement : IAuthorizationRequirement
    {
    }
    public class AuthorizationToUserOnly : AuthorizationHandler<EmptyAuthRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmptyAuthRequirement requirement)
        {
            var identity = context.User.Identity;

            if (identity?.Name == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            if (!identity.Name.Equals(((DefaultHttpContext)context.Resource).HttpContext.Request.RouteValues["id"]))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
