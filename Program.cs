﻿using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Flota365.Platform.API.Shared.Infrastructure.Persistence.EFC;
using Flota365.Platform.API.Shared.Infrastructure.Extensions;
using Flota365.Platform.API.IAM.Infrastructure.Extensions;
using Flota365.Platform.API.FleetManagement.Infrastructure.Extensions;
using Flota365.Platform.API.Personnel.Infrastructure.Extensions;
using Flota365.Platform.API.Maintenance.Infrastructure.Extensions;
using System.Text.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21)),
        mySqlOptions =>
        {
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }
    ));

// Add MediatR for CQRS
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

// Add Bounded Context Services
builder.Services.AddSharedServices();
builder.Services.AddIAMServices();
builder.Services.AddFleetManagementServices();
builder.Services.AddPersonnelServices();
builder.Services.AddMaintenanceServices();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",    // React dev
                "http://localhost:5173",    // Vite dev
                "https://flota365.com",     // Production domain
                "https://app.flota365.com"  // App subdomain
              )
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });

    // Fallback policy for development
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add Authentication & Authorization (placeholder for future JWT implementation)
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options => { /* JWT config */ });
// builder.Services.AddAuthorization();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Flota365 Platform API",
        Version = "v1.0",
        Description = "Fleet Management Platform - DDD Architecture with CQRS",
        Contact = new OpenApiContact
        {
            Name = "Flota365 Development Team",
            Email = "dev@flota365.com",
            Url = new Uri("https://github.com/flota365/platform")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Add security definition for future JWT implementation
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Include XML comments if available
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Group by bounded contexts
    c.TagActionsBy(api => new[] { GetBoundedContextFromPath(api.RelativePath) });
    c.DocInclusionPredicate((name, api) => true);
});

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>("database")
    .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy());

// Add Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

if (builder.Environment.IsProduction())
{
    builder.Logging.AddApplicationInsights(); // For Azure Application Insights
}

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flota365 Platform API v1");
        c.RoutePrefix = "swagger";
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
        c.ShowExtensions();
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    
    // Enable Swagger in production for API documentation
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flota365 Platform API v1");
        c.RoutePrefix = "api-docs";
    });
}

// Security headers
app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
    context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
    await next();
});

// CORS
app.UseCors(app.Environment.IsDevelopment() ? "AllowAll" : "AllowSpecificOrigins");

// Authentication & Authorization (when implemented)
// app.UseAuthentication();
// app.UseAuthorization();

// API Controllers
app.MapControllers();

// Health checks
app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

// Root endpoint
app.MapGet("/", () => new
{
    Service = "Flota365 Platform API",
    Version = "v1.0",
    Architecture = "DDD with CQRS",
    Environment = app.Environment.EnvironmentName,
    Timestamp = DateTime.UtcNow,
    BoundedContexts = new[]
    {
        "IAM - Identity & Access Management",
        "FleetManagement - Fleet and Vehicle Management", 
        "Personnel - Driver Management",
        "Maintenance - Maintenance and Service Records",
        "Analytics - Dashboard and Reports"
    },
    Endpoints = new
    {
        Swagger = "/swagger",
        Health = "/health",
        IAM = "/api/iam",
        FleetManagement = "/api/fleet-management", 
        Personnel = "/api/personnel",
        Maintenance = "/api/maintenance",
        Analytics = "/api/analytics"
    }
});

// Global exception handling
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        
        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            var logger = context.RequestServices.GetService<ILogger<Program>>();
            logger?.LogError(error.Error, "Unhandled exception occurred");
            
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
            {
                success = false,
                message = "An internal server error occurred",
                timestamp = DateTime.UtcNow,
                environment = app.Environment.EnvironmentName
            }));
        }
    });
});

// Database initialization
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        
        logger.LogInformation("Ensuring database is created...");
        await context.Database.EnsureCreatedAsync();
        
        if (app.Environment.IsDevelopment())
        {
            logger.LogInformation("Applying pending migrations...");
            await context.Database.MigrateAsync();
        }
        
        logger.LogInformation("Database initialization completed successfully");
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
        logger?.LogError(ex, "An error occurred while initializing the database");
        throw;
    }
}

app.Run();

// Helper method to determine bounded context from API path
static string GetBoundedContextFromPath(string? path)
{
    if (string.IsNullOrEmpty(path)) return "General";
    
    return path.ToLowerInvariant() switch
    {
        var p when p.Contains("/api/iam") => "IAM",
        var p when p.Contains("/api/fleet-management") => "Fleet Management",
        var p when p.Contains("/api/personnel") => "Personnel",
        var p when p.Contains("/api/maintenance") => "Maintenance", 
        var p when p.Contains("/api/analytics") => "Analytics",
        _ => "General"
    };
}