using Otel.Demo.VariableApi.Services.Interfaces;

namespace Otel.Demo.VariableApi.Services
{
    public class VariableService : IVariableService
    {
        private readonly ITelemetryService _telemetryService;

        public VariableService(ITelemetryService telemetryService)
        {
            _telemetryService = telemetryService;
        }

        public async Task<double> GetVariableValueFromVariableDB(string variableName)
        {
            using var activity_GetVariableValueFromVariableDB = _telemetryService.GetActivitySource().StartActivity("GetVariableValueFromVariableDB");
            Random random = new Random();
            int delay = random.Next(250, 2500);
            await Task.Delay(delay);
            return random.NextDouble()*10;
        }
    }
}
