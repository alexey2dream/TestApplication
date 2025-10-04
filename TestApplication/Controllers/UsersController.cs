using Microsoft.AspNetCore.Mvc;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Chats.Commands.DeleteChatCommand;
using TestApplication.UseCase.Users.Commands.BanUserCommand;
using TestApplication.UseCase.Users.Commands.ChangeUserUsernameCommand;
using TestApplication.UseCase.Users.Commands.CreateUserCommand;

namespace TestApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(
            [FromBody]CreateUserCommand command,
            [FromServices]ICommandHandler<CreateUserCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if(!result.IsSuccess)
                return BadRequest(result.Error);
            return Created();
        }
        [HttpPut("Username")]
        public async Task<IActionResult> UpdateUsername(
            [FromBody]ChangeUserUsernameCommand command,
            [FromServices]ICommandHandler<ChangeUserUsernameCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if(!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
        [HttpDelete("Ban")]
        public async Task<IActionResult> Ban(
            [FromQuery] BanUserCommand command,
            [FromServices] ICommandHandler<BanUserCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
    }
}
