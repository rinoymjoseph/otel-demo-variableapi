﻿using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using Otel.Demo.VariableApi.Models;
using Otel.Demo.VariableApi.Services.Interfaces;

namespace Otel.Demo.VariableApi.Controllers
{
    [Route("variable")]
    [ApiController]
    public class VariableController : ControllerBase
    {
        private readonly IVariableService _variableService;
        private readonly ITelemetryService _telemetryService;
        private ILogger _logger;

        public VariableController(ILogger<VariableController> logger, ITelemetryService telemetryService, IVariableService variableService)
        {
            _logger = logger;
            _telemetryService = telemetryService;
            _variableService = variableService;
        }

        [HttpGet("GetVariableData/{variableName}")]
        public async Task<IActionResult> GetVariableData(string variableName = "test")
        {
            _logger.LogInformation($"Entering GetVariableData : variable -> {variableName}");
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
            Baggage.SetBaggage("ContextId", contextId);

            try
            {
                var response = await _variableService.GetVariableValue(variableName);
                _telemetryService.GetVariableDataReqSuccessCounter().Add(1,
                    new("Action", nameof(GetVariableData)),
                    new("Controller", nameof(VariableController)));
                _logger.LogInformation($"Exiting GetVariableData : variable -> {variableName}");
                return Ok(response);
            }
            catch
            {
                _telemetryService.GetVariableDataReqFailureCounter().Add(1,
                    new("Action", nameof(GetVariableData)),
                    new("Controller", nameof(VariableController)));
                throw;
            }          
        }
    }
}
