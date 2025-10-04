using Microsoft.AspNetCore.Mvc;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Chats.Commands.DeleteChatCommand;
using TestApplication.UseCase.Users.Commands.BanUserCommand;
using TestApplication.UseCase.Users.Commands.ChangeUserUsernameCommand;
using TestApplication.UseCase.Users.Commands.CreateUserCommand;
using TestApplication.UseCase.Users.DTO;
using TestApplication.UseCase.Users.Queries.GetAllUsersQuery;
using TestApplication.UseCase.Users.Queries.GetTotalAmountUsersQuery;
using TestApplication.UseCase.Users.Queries.GetUserByIdQuery;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TestApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("TotalAmount")]
        public async Task<IActionResult> GetTotalAmount(
            [FromServices] IQueryHandler<GetTotalAmountUsersQuery, int> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(new GetTotalAmountUsersQuery(), token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(
            [FromQuery]GetAllUsersQuery query,
            [FromServices] IQueryHandler<GetAllUsersQuery, List<UserResponse>> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(query, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
        [HttpGet("ById")]
        public async Task<IActionResult> GetById(
            [FromQuery]GetUserByIdQuery query,
            [FromServices] IQueryHandler<GetUserByIdQuery, UserResponse> handler,
            CancellationToken token)
        {
            var result = await handler.Handle(query, token);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
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
