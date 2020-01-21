using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using WooliesXChallenge.Api.Features.Common;
using WooliesXChallenge.Api.Features.Users;
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

        [Fact]
        public async Task GetProducts_Returns200_WhenCalledWithAscendingSort()
        {
            var response = await _testContext.WooliesXApiClient.GetAsync("api/sort?sort=ascending");
            var products = await response.Content.ReadAsAsync<List<Product>>();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            products.Count.ShouldBe(5);
            products[0].Name.ShouldBe("Test Product A");
            products[4].Name.ShouldBe("Test Product F");
        }

        [Fact]
        public async Task GetProducts_Returns400_WhenCalledWithInvalidSort()
        {
            var response = await _testContext.WooliesXApiClient.GetAsync("api/sort?sort=invalid");

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
