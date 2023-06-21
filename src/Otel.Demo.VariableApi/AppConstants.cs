namespace Otel.Demo.VariableApi
{
    public class AppConstants
    {
        public const string OTEL_SERVCICE_NAME = "VariableApi";
        public const string URL_OTEL_EXPORTER = "otel_exporter_url";
        public const string URL_DATA_API = "data_api_url";

        public const string REQUEST_GET_VARIABLE_VALUE = "/variabledata/GetVariableValue";

        public const string COUNTER_VARIABLE_GET_VARIABLE_DATA_REQUESTS = "variable_api_get_variable_data_requests";
        public const string COUNTER_VARIABLE_GET_VARIABLE_DATA_REQUESTS_SUCCESS = "variable_api_get_variable_data_requests_success";
        public const string COUNTER_VARIABLE_GET_VARIABLE_DATA_REQUESTS_FAILURE = "variable_api_get_variable_data_requests_failure";
    }
}
