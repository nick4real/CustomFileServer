using CFS.Application;
using CFS.Infrastructure;
using CFS.Infrastructure.Options;
using CFS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

// Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbOptions>(builder.Configuration);

builder.AddServiceDefaults();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

// OpenAPI support
builder.Services.AddOpenApi();

// Application
var app = builder.Build();

app.MapDefaultEndpoints();

app.UseExceptionHandler(options => { });
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

if (!app.Environment.IsProduction())
{
    app.MapOpenApi();
    // Map the Scalar API reference endpoint
    app.MapScalarApiReference();
}

// Database initialization
await using (var serviceScope = app.Services.CreateAsyncScope())
await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
{
    if (!app.Environment.IsDevelopment())
        return;

    var executionStrategy = dbContext.Database.CreateExecutionStrategy();

    await executionStrategy.ExecuteAsync(async () =>
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    });
}

app.Run();
