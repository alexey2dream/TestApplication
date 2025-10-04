using Microsoft.AspNetCore.Mvc;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Chats.Commands.CreateChatCommand;
using TestApplication.UseCase.Chats.Commands.DeleteChatCommand;
using TestApplication.UseCase.Chats.Commands.DeleteChatMessageCommand;
using TestApplication.UseCase.Users.Commands.CreateUserCommand;

namespace TestApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateChat(
            [FromBody] CreateChatCommand command,
            [FromServices] ICommandHandler<CreateChatCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Created();
        }
        [HttpDelete]
        public async Task<IActionResult> CreateUser(
            [FromQuery] DeleteChatCommand command,
            [FromServices] ICommandHandler<DeleteChatCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
        [HttpDelete("Message")]
        public async Task<IActionResult> CreateUser(
            [FromQuery] DeleteChatMessageCommand command,
            [FromServices] ICommandHandler<DeleteChatMessageCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
    }
}
