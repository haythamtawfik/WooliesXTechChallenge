using Microsoft.AspNetCore.Mvc;

namespace WooliesXChallenge.Api.Features.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(_userService.GetUser());
        }
    }
}