using Catalog.Repositories;
using MongoDB.Driver;
using Catalog.Settings;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// usar guid con otra serializacion mas entendible
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
//usar datetime con otra serializacion mas entendible
BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));

//add singleton mongo client
builder.Services.AddSingleton<IMongoClient>(serviceProvider=>{
    var settings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
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
