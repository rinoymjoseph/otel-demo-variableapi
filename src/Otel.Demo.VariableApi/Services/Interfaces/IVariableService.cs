namespace Otel.Demo.VariableApi.Services.Interfaces
{
    public interface IVariableService
    {
        Task<double> GetVariableValueFromVariableDB(string variableName);
    }
}
