using CashTrackr.Application.Transactions.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace CashTrackr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpPost(Name = "CreateTransaction")]
        public async Task<IActionResult> CreateAsync([FromServices] Create useCase, [FromBody] CreateRequest request)
        {
            CreateResponse createResponse = await useCase.ExecuteAsync(request);

            return Ok(createResponse);
        }
    }
}
