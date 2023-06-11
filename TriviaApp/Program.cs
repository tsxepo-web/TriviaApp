using dotenv.net;
using MongoDB.Driver;
using TriviaApp.DataAccess;
using TriviaApp.Models;
using TriviaApp.Services;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
var envKeys = DotEnv.Read();
var mongoConnectionString = envKeys["ConnectionString"];
var mongoDatabaseName = envKeys["DatabaseName"];
var mongoCollectionName = envKeys["CollectionName"];
var mongoClient = new MongoClient(mongoConnectionString);
var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);
var mongoCollection = mongoDatabase.GetCollection<TriviaQuestions>(mongoCollectionName);
builder.Services.AddSingleton(mongoCollection);
builder.Services.AddScoped<ITriviaAppRepository, TriviaAppRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();