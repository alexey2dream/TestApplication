using Microsoft.AspNetCore.Mvc;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Channels.Commands.CreateChannelCommand;
using TestApplication.UseCase.Chats.Commands.CreateChatCommand;

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

    }
}
