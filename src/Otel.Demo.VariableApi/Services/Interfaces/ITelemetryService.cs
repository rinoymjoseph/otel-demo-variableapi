using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Otel.Demo.VariableApi.Services.Interfaces
{
    public interface ITelemetryService
    {
        ActivitySource GetActivitySource();

        Counter<long> GetVariableDataReqCounter();
    }
}
