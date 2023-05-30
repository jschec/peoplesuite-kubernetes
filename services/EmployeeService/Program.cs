using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;

using EmployeeService.Models;
using EmployeeService.Utils;

var builder = WebApplication.CreateBuilder(args);

// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

StorageConfig storageConfig = config.GetRequiredSection("Storage")
    .Get<StorageConfig>();

////////////////////////////////////
// Add services to the container. //
////////////////////////////////////

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AWS Services configuration
AWSOptions awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);
// Add DynamoDB
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
// Add S3
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddScoped<IS3Service, S3Service>(
    _ => new S3Service(storageConfig));

////////////////////////////////////

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

app.Run();