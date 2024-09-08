using Cafe.API.Middleware;
using Cafe.Persistance;
using Cafe.Persistance.EFCustomizations;
using Cafe.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
var config = builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>(optional: true)
    .Build();

builder.Services.RegisterPersistenceServices(config);
builder.Services.RegisterApplicationServices();

builder.Services.Configure<ApplicationConfig>(
    builder.Configuration.GetSection(nameof(ApplicationConfig)));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Add(ServiceDescriptor.Singleton(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>)));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope
        .ServiceProvider
        .GetRequiredService<CafeDbContext>();
    dbContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
