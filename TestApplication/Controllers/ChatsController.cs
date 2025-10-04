using Microsoft.AspNetCore.Mvc;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Chats.Commands.CreateChatCommand;
using TestApplication.UseCase.Chats.Commands.CreateChatMessageCommand;
using TestApplication.UseCase.Chats.Commands.DeleteChatCommand;
using TestApplication.UseCase.Chats.Commands.DeleteChatMessageCommand;
using TestApplication.UseCase.Chats.Commands.UpdateChatTitleCommand;
using TestApplication.UseCase.Users.Commands.ChangeUserUsernameCommand;
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
        [HttpPut("Title")]
        public async Task<IActionResult> UpdateTitle(
            [FromBody] UpdateChatTitleCommand command,
            [FromServices] ICommandHandler<UpdateChatTitleCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(
            [FromQuery] DeleteChatCommand command,
            [FromServices] ICommandHandler<DeleteChatCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
        [HttpPost("Message")]
        public async Task<IActionResult> CreateMessage(
            [FromBody] CreateChatMessageCommand command,
            [FromServices] ICommandHandler<CreateChatMessageCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Created();
        }
        [HttpDelete("Message")]
        public async Task<IActionResult> DeleteMessage(
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
