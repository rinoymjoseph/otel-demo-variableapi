using Otel.Demo.VariableApi.Services.Interfaces;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Otel.Demo.VariableApi.Services
{
    public class TelemetryService : ITelemetryService
    {
        private readonly ActivitySource _activitySource;
        public static Meter _meter = new(AppConstants.OTEL_SERVCICE_NAME);
        private readonly Counter<long> _getVariableDataReqCounter;

        public TelemetryService()
        {
            _activitySource = new ActivitySource(AppConstants.OTEL_SERVCICE_NAME);
            _getVariableDataReqCounter = _meter.CreateCounter<long>(AppConstants.COUNTER_VARIABLE_GET_VARIABLE_DATA);
        }

        public ActivitySource GetActivitySource()
        {
            return _activitySource;
        }

        public Counter<long> GetVariableDataReqCounter()
        {
            return _getVariableDataReqCounter;
        }
    }
}
