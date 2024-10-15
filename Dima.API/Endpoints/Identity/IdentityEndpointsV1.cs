using Dima.API.Common.Api;
using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Dima.API.Endpoints.Identity;

public class IdentityEndpointsV1
{
    public class LogoutEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/logout", HandleAsync);

        private static async Task<IResult> HandleAsync(SignInManager<Models.User> signInManager)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
    }

    public class RolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/roles", Handle);

        private static IResult Handle(ClaimsPrincipal user)
        {
            if (user.Identity is null || !user.Identity.IsAuthenticated)
                return Results.Unauthorized();

            var identity = (ClaimsIdentity)user.Identity;

            var roles = identity
                .FindAll(identity.RoleClaimType)
                .Select(c => new RoleClaim
                {
                    Issuer = c.Issuer,
                    OriginalIssuer = c.OriginalIssuer,
                    Type = c.Type,
                    Value = c.Value,
                    ValueType = c.ValueType
                });

            return Results.Ok(roles);
        }
    }
}
