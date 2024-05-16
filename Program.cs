using HWTechnicalTest.FTApi;
using HWTechnicalTest.Interfaces;
using HWTechnicalTest.Services;
using HWTechnicalTest.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<FTApiSettings>(builder.Configuration.GetSection("FTAPI"));

builder.Services.AddSingleton<IDBOffersService, MongoDBOffersService>();
builder.Services.AddSingleton<FTAPIClient>();
builder.Services.AddHostedService<OffersUpdateBackgroundService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();
