using Microsoft.AspNetCore.Mvc;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Chats.Commands.CreateChatCommand;
using TestApplication.UseCase.Chats.Commands.CreateChatMessageCommand;
using TestApplication.UseCase.Chats.Commands.DeleteChatCommand;
using TestApplication.UseCase.Chats.Commands.DeleteChatMessageCommand;
using TestApplication.UseCase.Chats.Commands.UpdateChatTitleCommand;
using TestApplication.UseCase.Chats.DTO;
using TestApplication.UseCase.Chats.Queries.GetAllMessagesByChatQuery;
using TestApplication.UseCase.Chats.Queries.GetUserAllChatsQuery;
using TestApplication.UseCase.Users.Commands.ChangeUserUsernameCommand;
using TestApplication.UseCase.Users.Commands.CreateUserCommand;
using TestApplication.UseCase.Users.DTO;
using TestApplication.UseCase.Users.Queries.GetAllUsersQuery;

namespace TestApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        [HttpGet("ByUserId")]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllChatsByUserQuery query,
            [FromServices] IQueryHandler<GetAllChatsByUserQuery, List<ChatResponse>> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(query, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
        [HttpGet("Messages")]
        public async Task<IActionResult> GetAllMessagesByChat(
            [FromQuery] GetChatWithAllMessagesQuery query,
            [FromServices] IQueryHandler<GetChatWithAllMessagesQuery, ChatInfoResponse> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(query, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
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
