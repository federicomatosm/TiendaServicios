using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TiendaServicios.Api.Gateway.ImplementRemote;
using TiendaServicios.Api.Gateway.MessageHandler;
using TiendaServicios.Api.Gateway.RemoteInterface;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"ocelot.json");
// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddOcelot().AddDelegatingHandler<LibroHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("AutorService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Autor"]);
});

builder.Services.AddSingleton<IAutorRemote, AutorRemote>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
await app.UseOcelot();
//app.MapControllers();

app.Run();

