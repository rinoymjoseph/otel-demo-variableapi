﻿using Otel.Demo.VariableApi.Models;
using Otel.Demo.VariableApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;

namespace Otel.Demo.VariableApi.Controllers
{
    [Route("variable")]
    [ApiController]
    public class VariableController : ControllerBase
    {
        private readonly IVariableService _variableService;
        private readonly ITelemetryService _telemetryService;

        public VariableController(ITelemetryService telemetryService, IVariableService variableService)
        {
            _telemetryService = telemetryService;
            _variableService = variableService;
        }

        [HttpGet("GetVariableData/{variableName}")]
        public async Task<IActionResult> GetVariableData(string variableName="test")
        {
            _telemetryService.GetVariableDataReqCounter().Add(1,
                new("Action", nameof(GetVariableData)),
                new("Controller", nameof(VariableController)));

            var contextId = Baggage.GetBaggage("ContextId");
            if (string.IsNullOrEmpty(contextId))
            {
                contextId = Guid.NewGuid().ToString();
            }
            using var activity_GetVariableValue = _telemetryService.GetActivitySource().StartActivity("GetVariableData");
            activity_GetVariableValue?.SetTag("VariableName", variableName);
            activity_GetVariableValue?.SetTag("ContextId", contextId);
            activity_GetVariableValue?.AddEvent(new("GetVariableData"));
            Baggage.SetBaggage("ContextId", contextId);
            var value = await _variableService.GetVariableValueFromVariableDB(variableName);
            VariableData variableData = new VariableData();
            variableData.Name = variableName;
            variableData.Value = value;
            return Ok(variableData);      
        }
    }
}