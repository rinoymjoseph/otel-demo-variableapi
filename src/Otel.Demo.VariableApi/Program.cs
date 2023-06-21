using Otel.Demo.VariableApi;
using Otel.Demo.VariableApi.Services;
using Otel.Demo.VariableApi.Services.Interfaces;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using Otel.Demo.VariableApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITelemetryService, TelemetryService>();
builder.Services.AddScoped<IVariableService, VariableService>();

string otelExporterUrl = builder.Configuration.GetValue<string>(AppConstants.OTEL_EXPORTER_URL);
bool enableOtelLogging = builder.Configuration.GetValue<bool>(AppConstants.OTEL_ENABLE_LOGGING);
bool enableOtelTracing = builder.Configuration.GetValue<bool>(AppConstants.OTEL_ENABLE_TRACING);
bool enableOtelMetrics = builder.Configuration.GetValue<bool>(AppConstants.OTEL_ENABLE_METRICS);

if (enableOtelLogging)
{
    builder.Services
    .AddLogging((loggingBuilder) => loggingBuilder
    .AddOpenTelemetry(options =>
        options
            .AddConsoleExporter()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(otelExporterUrl);
            })));
}

if (enableOtelTracing)
{
    builder.Services
        .AddOpenTelemetry()
        .ConfigureResource(builder => builder
        .AddService(serviceName: AppConstants.OTEL_SERVCICE_NAME))
        .WithTracing(builder => builder
            .AddSource(AppConstants.OTEL_SERVCICE_NAME)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation() //Required for baggage
            .AddConsoleExporter()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(otelExporterUrl);
            }));
}

if (enableOtelMetrics)
{
    builder.Services
        .AddOpenTelemetry()
        .ConfigureResource(builder => builder
        .AddService(serviceName: AppConstants.OTEL_SERVCICE_NAME))
        .WithMetrics(metricsProviderBuilder => metricsProviderBuilder
            .ConfigureResource(resource => resource
            .AddService(AppConstants.OTEL_SERVCICE_NAME))
            .AddMeter(TelemetryService._meter.Name)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(otelExporterUrl);
            }));
}

builder.Services.AddHttpClient();

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
