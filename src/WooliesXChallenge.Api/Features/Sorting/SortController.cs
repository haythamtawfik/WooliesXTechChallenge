using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WooliesXChallenge.Api.Features.Common;

namespace WooliesXChallenge.Api.Features.Sorting
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly ISortService _sortService;
        public SortController(ISortService sortService)
        {
            _sortService = sortService;
        }

        /// <summary>
        /// Gets a list of products sorted by price (low to high or high to low), product name ( ascending or descending)
        /// or recommended sort which sorts the products according to their popularity
        /// </summary> 
        /// <param name="sort">
        /// A unique identifier for this call to the API.
        /// </param>
        /// <returns></returns>
        /// <response code="200">Returns a list of products</response>
        /// <response code="400">The request is invalid</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProducts([FromQuery]string sort)
        {
            var (products, errors) = await _sortService.GetSortedProducts(sort);

            if (errors != null)
            {
                return ValidationProblem(errors);
            }

            return Ok(products);

        }
    }
}