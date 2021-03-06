﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using WooliesXChallenge.Api.Features.Users;

namespace WooliesXChallenge.Tests
{
    [Collection("wooliesx")]
    public class UserControllerTests
    {
        private readonly TestContext _testContext;

        public UserControllerTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task GetUser_Returns200_WhenCalled()
        {
            var response = await _testContext.WooliesXApiClient.GetAsync("api/user");
            var user = await response.Content.ReadAsAsync<GetUserResponse>();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            user.Name.ShouldBe( "Haytham Tawfik");
            user.Token.ShouldBe("45ab025d-e470-4a0b-9d98-2b7da52fd137");
        }
    }
}
