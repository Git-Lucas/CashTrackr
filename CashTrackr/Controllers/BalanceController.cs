using CashTrackr.Application.Balances.Queries.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CashTrackr.Controllers;

[ApiController]
[Route("[controller]")]
public class BalanceController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDailyBalanceAsync([FromServices] GetDailyBalanceQueryHandler handler, [FromQuery] DateOnly date)
    {
        decimal dailyBalance = await handler.HandleAsync(date);
        
        return Ok(dailyBalance);
    }
}
