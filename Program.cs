using Microsoft.EntityFrameworkCore;
using Tienda_don_Pedro.Data;
using Tienda_don_Pedro.GraphQL.Mutations;
using Tienda_don_Pedro.GraphQL.Queries;
using Tienda_don_Pedro.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<ProductoQuery>()
    .AddMutationType<ProductoMutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .RegisterDbContext<AppDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGraphQL();

app.Run();
