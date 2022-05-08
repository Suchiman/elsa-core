using Elsa.Api.ApiResults;
using Elsa.Api.Models;
using Elsa.AspNetCore;
using Elsa.Modules.Activities.Primitives;
using Elsa.Runtime.Models;
using Elsa.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elsa.Api.Endpoints.Events;

[Area(AreaNames.Elsa)]
[ApiEndpoint(ControllerNames.Events, "Trigger")]
[ProducesResponseType(typeof(ProcessStimulusResults), StatusCodes.Status200OK, "application/json")]
public class Trigger : Controller
{
    private readonly IHasher _hasher;
    public Trigger(IHasher hasher) => _hasher = hasher;

    [HttpPost]
    public IActionResult Handle(string eventName)
    {
        var hash = _hasher.Hash(eventName);
        var stimulus = Stimulus.Standard(nameof(Event), hash);
        return new ProcessStimulusResult(stimulus);
    }
}