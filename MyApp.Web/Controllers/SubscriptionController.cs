using Microsoft.AspNetCore.Mvc;
using MyApp.Modules.Subscriptions.DTO;
using MyApp.Modules.Subscriptions.Services;

namespace MyApp.Web.Controllers;

[ApiController]
[Route("api/subscriptions")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost("")]
    public IActionResult Subscribe([FromBody] SubscribeDTO dto)
    {
        var subscriptionId = _subscriptionService.Subscribe(dto);

        return Ok(new { id = subscriptionId });
    }

    [HttpDelete("{id}")]
    public IActionResult Unsubscribe(string id)
    {
        _subscriptionService.Unsubscribe(id);

        return NoContent();
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] SubscriptionSearchDTO dto)
    {
        var response = _subscriptionService.Search(dto);

        return new JsonResult(response);
    }
}
