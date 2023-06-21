using Otel.Demo.VariableApi.Models;

namespace Otel.Demo.VariableApi.Services.Interfaces
{
    public interface IVariableService
    {
        Task<VariableData> GetVariableValue(string variableName);
    }
}
