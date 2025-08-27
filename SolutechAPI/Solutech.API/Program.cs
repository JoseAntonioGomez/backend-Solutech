using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Solutech.Data.Models;
using Solutech.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionSQLServer")));

builder.Services.AddScoped<UsuarioLogic>();
builder.Services.AddScoped<TareaLogic>();

builder.Services.AddEndpointsApiExplorer();

// Genera el documento Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SolutechAPI",
        Version = "v1",
        Description = "API Solutech prueba con Swagger"
    });
});

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SolutechAPI v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

