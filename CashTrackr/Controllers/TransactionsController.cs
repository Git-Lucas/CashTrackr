using CashTrackr.Application.Transactions.Commands.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CashTrackr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        [HttpPost(Name = "CreateTransaction")]
        public async Task<IActionResult> CreateAsync([FromServices] CreateCommandHandler handler, [FromBody] CreateRequest request)
        {
            CreateResponse createResponse = await handler.HandleAsync(request);

            return Ok(createResponse);
        }
    }
}
