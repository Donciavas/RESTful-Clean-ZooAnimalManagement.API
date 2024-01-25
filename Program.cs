using FastEndpoints;
using FastEndpoints.Swagger;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Database;
using ZooAnimalManagement.API.Repositories;
using ZooAnimalManagement.API.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new SqliteConnectionFactory(config.GetValue<string>("Database:ConnectionString")));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IAnimalRepository, AnimalRepository>();
builder.Services.AddSingleton<IAnimalService, AnimalService>();
builder.Services.AddSingleton<IEnclosureRepository, EnclosureRepository>();
builder.Services.AddSingleton<IEnclosureService, EnclosureService>();
builder.Services.AddSingleton<IEnclosureAssignmentService, EnclosureAssignmentService>();

var app = builder.Build();

app.UseFastEndpoints(x =>
{
    x.Errors.ResponseBuilder = (failures, ctx, statusCode) =>
    {
        return new ValidationFailureResponse
        {
            Errors = failures.Select(y => y.ErrorMessage).ToList()
        };
    };
});

app.UseOpenApi();
app.UseSwaggerUi(s => s.ConfigureDefaults());

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();
