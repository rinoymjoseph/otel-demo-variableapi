using Otel.Demo.VariableApi.Models;
using Otel.Demo.VariableApi.Services.Interfaces;

namespace Otel.Demo.VariableApi.Services
{
    public class VariableService : IVariableService
    {
        private ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ITelemetryService _telemetryService;

        public VariableService(ILogger<VariableService> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory,
           ITelemetryService telemetryService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _telemetryService = telemetryService;
        }

        public async Task<VariableData> GetVariableValue(string variableName)
        {
            _logger.LogInformation($"Entering GetVariableValue : variable -> {variableName}");
            using var activity_GetVariables = _telemetryService.GetActivitySource().StartActivity("GetVariableValue");
            var dataApiUrl = _configuration.GetValue<string>(AppConstants.URL_DATA_API);
            var request = new HttpRequestMessage(HttpMethod.Get, $"{dataApiUrl}{AppConstants.REQUEST_GET_VARIABLE_VALUE}/{variableName}");
            var httpClient = _httpClientFactory.CreateClient();
            var httpResult = await httpClient.SendAsync(request);
            var response = await httpResult.Content.ReadAsStringAsync();
            httpResult.EnsureSuccessStatusCode();
            VariableData variableData = new VariableData();
            variableData.Name = variableName;
            variableData.Value = Convert.ToDouble(response);
            _logger.LogInformation($"Exiting GetVariableValue : variable -> {variableName}");
            return variableData;
        }
    }
}
