using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RssReaders.Infrastructure.Commands;
using RssReaders.Infrastructure.Services;

namespace RssReaders.Api.Controllers
{
    [Route("[Controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync (ObjectId userId)
            => Json (await _userService.GetAccountAsync(userId));

        [HttpPost("register")]           
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            await _userService.RegisterAsync(ObjectId.GenerateNewId(),
                command.Email, command.Username, command.Password, command.Role);

            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));

    }
}