using Catalog.Repositories;
using MongoDB.Driver;
using Catalog.Settings;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// usar guid con otra serializacion mas entendible
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
//usar datetime con otra serializacion mas entendible
BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));

//add singleton mongo client
var mogoDbsettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
builder.Services.AddSingleton<IMongoClient>(serviceProvider=>{
    return new MongoClient(mogoDbsettings.ConnectionString);
});
//add singleton para usar el guardado en memoria
//builder.Services.AddSingleton<IItemsEnMemoria, ItemsEnMemoria>();
//singleto para usar mongo
builder.Services.AddSingleton<IItemsEnMemoria, MongoDbItemRepository>();

builder.Services.AddControllers(Options=>{
    Options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
.AddMongoDb(
    mogoDbsettings.ConnectionString,
    name: "mongodb",
    timeout:TimeSpan.FromSeconds(3),
    tags: new [] { "ready" });

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

/*
app.UseHealthChecks("/health");
*/

app.MapHealthChecks("/health/ready", new HealthCheckOptions{
    Predicate = (check) => check.Tags.Contains("ready"),
    /*
    ResponseWriter = async(context, report)=>
    {
        var result = JsonSerializer.Serialize(
            new {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new {
                    name = e.Key,
                    status = e.Value.Status.ToString(),
                    exception = e.Value.Exception != null?e.Value.Exception.Message:"none",
                    duration = e.Value.Duration.ToString()
                })
            });
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(result);
    }
    */
});

app.MapHealthChecks("/health/live", new HealthCheckOptions{
    Predicate = (_) => false
});



app.Run();
