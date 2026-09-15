using Microsoft.EntityFrameworkCore;
using NLayers.BusinessLogic.Interfaces;
using NLayers.BusinessLogic.Managers;
using NLayers.BusinessLogic.Services;
using NLayers.DataAccess;
using NLayers.DataAccess.Interfaces;
using NLayers.DataAccess.Stores;
using NLayers.Presentation.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers and discover controllers from NLayers.Presentation layer
builder.Services.AddControllers()
    .AddApplicationPart(typeof(ProductController).Assembly);

// Configure Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (!string.IsNullOrWhiteSpace(connectionString) && !connectionString.Contains("YOUR_SERVER_NAME"))
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    // Fallback in-memory database for immediate out-of-the-box local testing without prior SQL Server setup
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("NLayersDb"));
}

// Register Dependency Injection for Architecture Layers
builder.Services.AddScoped<IProductStore, ProductStore>();
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Configure OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Ensure database is created
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
