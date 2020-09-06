using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Shouldly;
using WooliesXChallenge.Api.Features.Common;
using Xunit;

namespace WooliesXChallenge.Tests
{
    [Collection("wooliesx")]
    public class SortControllerTests
    {
        private readonly TestContext _testContext;
        public SortControllerTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Theory]
        [InlineData("ascending","Test Product A", "Test Product F")]
        [InlineData("descending", "Test Product F", "Test Product A")]
        [InlineData("High", "Test Product F", "Test Product D")]
        [InlineData("Low", "Test Product D", "Test Product F")]
        [InlineData("Recommended", "Test Product A", "Test Product D")]
        public async Task GetProducts_Returns200_WhenCalledWithAscendingSort(string sortOption,string firstProduct, string lastProduct)
        {
            var response = await _testContext.WooliesXApiClient.GetAsync($"api/sort?sortOption={sortOption}");
            var products = await response.Content.ReadAsAsync<List<Product>>();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            products.Count.ShouldBe(5);
            products[0].Name.ShouldBe(firstProduct);
            products[4].Name.ShouldBe(lastProduct);
        }

        [Fact]
        public async Task GetProducts_Returns400_WhenCalledWithInvalidSort()
        {
            var response = await _testContext.WooliesXApiClient.GetAsync("api/sort?sortOption=invalid");

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
