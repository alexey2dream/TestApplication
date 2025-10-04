using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Models;
using TestApplication.Domain.Users.Repositories;
using TestApplication.Domain.Users.Services;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Users.Commands.CreateUserCommand
{
    public class CreateUserCommand : ICommand
    {
        public string Username { get; set; }
    }
    public class CreateUserCommandHandler(
        IUserRep rep,
        UserService service)
        : ICommandHandler<CreateUserCommand>
    {
        public async Task<Result> Handle(CreateUserCommand command, CancellationToken token = default)
        {
            var result = await service.Create(command.Username, token);
            if (!result.IsSuccess)
                return result;
            if (!await rep.Add(result.Value, token))
                return Result.Failure("Invalid add!");
            if(rep.Save() == 0)
                return Result.Failure("Invalid save!");
            return Result.Success();
        }
    }
}
