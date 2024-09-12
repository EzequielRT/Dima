using Dima.API.Data;
using Dima.API.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Category;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryHandler, CategoryHandler>();

var cnnStr = builder.Configuration.GetConnectionString("DafaultConnection");
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost(
    "/v1/categories",
    async (CreateCategoryRequest request, ICategoryHandler handler)
    => await handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<CreateCategoryResponse>();

app.MapPut(
    "/v1/categories/{id}",
    async ([AsParameters] UpdateCategoryRequest request, ICategoryHandler handler)
    => await handler.UpdateAsync(request))
    .WithName("Categories: Update")
    .WithSummary("Atualiza uma categoria")
    .Produces<UpdateCategoryResponse>();

app.MapDelete(
    "/v1/categories/{id}",
    async ([AsParameters] DeleteCategoryRequest request, ICategoryHandler handler)
    => await handler.DeleteAsync(request))
    .WithName("Categories: Delete")
    .WithSummary("Excluí uma categoria")
    .Produces<DeleteCategoryResponse>();

app.MapGet(
    "/v1/categories/{id}",
    async ([AsParameters] GetCategoryByIdRequest request, ICategoryHandler handler)
    => await handler.GetByIdAsync(request))
    .WithName("Categories: Get By Id")
    .WithSummary("Retorna uma categoria pelo Id")
    .Produces<GetCategoryByIdResponse>();

app.MapGet(
    "/v1/categories",
    async ([AsParameters] GetAllCategoriesRequest request, ICategoryHandler handler)
    => await handler.GetAllAsync(request))
    .WithName("Categories: Get All")
    .WithSummary("Retorna todas as categorias")
    .Produces<GetAllCategoriesResponse>();

app.Run();