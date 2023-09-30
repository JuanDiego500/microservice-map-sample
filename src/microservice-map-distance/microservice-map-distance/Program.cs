using Azure.Identity;
using GoogleMapInfo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<GoogleDistanceApi>();
builder.Services.AddControllers();
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

app.Run();
