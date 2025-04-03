using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Scf;
using Scf.Servico.ApplicationCore;
using Scf.Servico.Infrastructure.Common;
using Scf.Servico.Infrastructure.Sql;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add dependencies.
builder.Services.RegisterCoreDependencies();
builder.Services.RegisterCommonDependencies(builder.Configuration);
builder.Services.RegisterSqlDependencies(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
   .SetBasePath(builder.Environment.ContentRootPath)
   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
   .AddEnvironmentVariables();

var strConn = builder.Configuration.GetValue<string>("ConnectionStrings:Context");

builder.Services.AddSingleton(new DatabaseConfig { Nome = strConn });

var ambiente = builder.Environment.EnvironmentName;
builder.Services.AddSingleton(new EnvConfig { Ambiente = ambiente });

builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<EnumSchemaFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Policy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddHealthChecks();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Serviço de Controle de Lançamentos", Version = "V.0.1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Informe o Bearer Token. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Policy");

app.UseHttpsRedirection();

app.MapControllers();

builder.Services.AddHealthChecks();

app.MapHealthChecks("/health", // <= health check url
    new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {

            var result = JsonSerializer.Serialize(
                new
                {
                    statusApplication = report.Status.ToString(),
                    currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Environment = app.Environment.EnvironmentName
                });

            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.Response.WriteAsync(result);
        }
    });

app.Run();