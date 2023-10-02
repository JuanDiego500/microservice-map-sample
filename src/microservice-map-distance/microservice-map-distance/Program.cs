using Azure.Identity;
using GoogleMapInfo;
using microservice_map_distance.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<GoogleDistanceApi>();
builder.Services.AddControllers();
builder.Services.AddGrpc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Uri azurekeyVaultUrl = new(builder.Configuration.GetSection("KeyVaultUrl").Value!);

DefaultAzureCredential defaultAzureCredential = new();
builder.Configuration.AddAzureKeyVault(azurekeyVaultUrl, defaultAzureCredential);

//var apikey = builder.Configuration.GetSection("googledistanceapikey").Value!;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<DistanceInfoService>();

app.Run();
