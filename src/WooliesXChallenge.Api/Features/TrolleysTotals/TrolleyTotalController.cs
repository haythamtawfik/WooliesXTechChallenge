using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WooliesXChallenge.Api.Features.Common;

namespace WooliesXChallenge.Api.Features.TrolleysTotals
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyTotalController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IResourcesApiClient _resourcesApiClient;

        public TrolleyTotalController(IResourcesApiClient resourcesApiClient)
        {
            _logger = Log.ForContext<TrolleyTotalController>();
            _resourcesApiClient = resourcesApiClient;
        }
        
        /// <summary>
        /// Calculates the minimum possible total for a trolley given 
        /// </summary> 
        /// <param name="request">
        /// Trolley total request which has a list of products, quantities and specials which are then
        /// used to calculate the minimum possible total for the trolley
        /// </param>
        /// <returns></returns>
        /// <response code="200">Returns a list of products</response>
        /// <response code="400">The request is invalid</response>
        [HttpPost]
        public async Task<IActionResult> CalculateLowestPossibleTotal(
            [FromBody] TrolleyTotalRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.Information("Invalid Request");
                return ValidationProblem(ModelState);
            }

            var total = await _resourcesApiClient.CalculateMinTrolleyTotal(request);

            return Ok(total);
        }
    }
}
