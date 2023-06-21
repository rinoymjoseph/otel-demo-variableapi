namespace Otel.Demo.VariableApi
{
    public class AppConstants
    {
        public const string OTEL_SERVCICE_NAME = "VariableApi";
        public const string OTEL_EXPORTER_URL = "Otel:ExporterUrl";
        public const string OTEL_ENABLE_LOGGING = "Otel:EnableLogging";
        public const string OTEL_ENABLE_TRACING = "Otel:EnableTracing";
        public const string OTEL_ENABLE_METRICS = "Otel:EnableMetrics";

        public const string DATA_API_URL = "DataApiUrl";

        public const string REQUEST_GET_VARIABLE_VALUE = "/variabledata/GetVariableValue";

        public const string COUNTER_VARIABLE_GET_VARIABLE_DATA_REQUESTS = "variable_api_get_variable_data_requests";
        public const string COUNTER_VARIABLE_GET_VARIABLE_DATA_REQUESTS_SUCCESS = "variable_api_get_variable_data_requests_success";
        public const string COUNTER_VARIABLE_GET_VARIABLE_DATA_REQUESTS_FAILURE = "variable_api_get_variable_data_requests_failure";
    }
}
