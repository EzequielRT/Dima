using Dima.API.Data;
using Dima.API.Endpoints;
using Dima.API.Handlers;
using Dima.API.Models;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryHandler, CategoryHandler>();
builder.Services.AddScoped<ITransactionHandler, TransactionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.AddAuthorization();

var cnnStr = builder.Configuration.GetConnectionString("DafaultConnection");
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr);
});

builder.Services
    .AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => new { message = "OK"});

app.MapEndpoints();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapPost("logout", async (SignInManager<User> signInManager) =>
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    })
    .RequireAuthorization();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapPost("roles", async (ClaimsPrincipal user) =>
    {
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Results.Unauthorized();

        var identity = (ClaimsIdentity)user.Identity;

        var roles = identity
            .FindAll(identity.RoleClaimType)
            .Select(c => new 
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });

        return Results.Ok(roles);
    })
    .RequireAuthorization();

app.Run();