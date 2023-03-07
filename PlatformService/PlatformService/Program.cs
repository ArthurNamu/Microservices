using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration; ;
IWebHostEnvironment env = builder.Environment;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

ConnectionService.Set(configuration);

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

//if (env.IsProduction())
//{
    Console.WriteLine("--> Using SQL Server DB");

    builder.Services.AddDbContext<AppDBContext>(opt =>
    opt.UseSqlServer(configuration.GetConnectionString("PlatformsConn")));
//}
//else
//{
//    Console.WriteLine("--> Using InMemory DB");
//    builder.Services.AddDbContext<AppDBContext>(opt =>
//                        opt.UseInMemoryDatabase("InMem"));
//}

Console.WriteLine($"CommensDervice Endpoint {configuration["CommandService"]}");

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app, env.IsProduction());

app.Run();
