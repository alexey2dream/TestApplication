using Microsoft.AspNetCore.Mvc;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Channels.Commands.ChangeTitleChannelCommand;
using TestApplication.UseCase.Channels.Commands.CreateChannelCommand;
using TestApplication.UseCase.Channels.Commands.CreateChannelMessageCommand;
using TestApplication.UseCase.Channels.Commands.DeleteChannelCommand;
using TestApplication.UseCase.Channels.Commands.DeleteChannelMessageCommand;
using TestApplication.UseCase.Chats.Commands.CreateChatCommand;
using TestApplication.UseCase.Chats.Commands.CreateChatMessageCommand;
using TestApplication.UseCase.Chats.Commands.DeleteChatCommand;
using TestApplication.UseCase.Chats.Commands.DeleteChatMessageCommand;
using TestApplication.UseCase.Chats.Commands.UpdateChatTitleCommand;

namespace TestApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateChannel(
            [FromBody] CreateChannelCommand command,
            [FromServices] ICommandHandler<CreateChannelCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Created();
        }
        [HttpPut("Title")]
        public async Task<IActionResult> UpdateTitle(
            [FromBody] ChangeTitleChannelCommand command,
            [FromServices] ICommandHandler<ChangeTitleChannelCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(
            [FromQuery] DeleteChannelCommand command,
            [FromServices] ICommandHandler<DeleteChannelCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
        [HttpPost("Message")]
        public async Task<IActionResult> CreateMessage(
            [FromBody] CreateChannelMessageCommand command,
            [FromServices] ICommandHandler<CreateChannelMessageCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Created();
        }
        [HttpDelete("Message")]
        public async Task<IActionResult> DeleteMessage(
            [FromQuery] DeleteChannelMessageCommand command,
            [FromServices] ICommandHandler<DeleteChannelMessageCommand> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(command, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
    }
}
