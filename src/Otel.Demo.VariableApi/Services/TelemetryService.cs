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
        private readonly Counter<long> _getVariableDataReqSuccessCounter;
        private readonly Counter<long> _getVariableDataReqFailureCounter;

        public TelemetryService()
        {
            _activitySource = new ActivitySource(AppConstants.OTEL_SERVCICE_NAME);
            _getVariableDataReqCounter = _meter.CreateCounter<long>(AppConstants.COUNTER_VARIABLE_GET_VARIABLE_DATA_REQUESTS);
            _getVariableDataReqSuccessCounter = _meter.CreateCounter<long>(AppConstants.COUNTER_VARIABLE_GET_VARIABLE_DATA_REQUESTS_SUCCESS);
            _getVariableDataReqFailureCounter = _meter.CreateCounter<long>(AppConstants.COUNTER_VARIABLE_GET_VARIABLE_DATA_REQUESTS_FAILURE);
        }

        public ActivitySource GetActivitySource()
        {
            return _activitySource;
        }

        public Counter<long> GetVariableDataReqCounter()
        {
            return _getVariableDataReqCounter;
        }

        public Counter<long> GetVariableDataReqSuccessCounter()
        {
            return _getVariableDataReqSuccessCounter;
        }

        public Counter<long> GetVariableDataReqFailureCounter()
        {
            return _getVariableDataReqFailureCounter;
        }
    }
}
