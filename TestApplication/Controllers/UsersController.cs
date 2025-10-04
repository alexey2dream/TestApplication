using Microsoft.AspNetCore.Mvc;
using TestApplication.UseCase.Abstractions.Messaging;
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
    }
}
