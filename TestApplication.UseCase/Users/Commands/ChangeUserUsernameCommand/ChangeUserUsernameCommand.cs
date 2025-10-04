using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Repositories;
using TestApplication.Domain.Users.Services;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Users.Commands.ChangeUserUsernameCommand
{
    public class ChangeUserUsernameCommand : ICommand
    {
        public int Id { get; set; }
        public string NewUsername { get; set; }
    }
    public class ChangeUserUsernameCommandHandler
        (IUserRep rep,
        UserService service)
        : ICommandHandler<ChangeUserUsernameCommand>
    {
        public async Task<Result> Handle(ChangeUserUsernameCommand command, CancellationToken token = default)
        {
            var user = await rep.GetById(command.Id, token);
            if (user == null)
                return Result.Failure("User not exists!");
            var result = await service.ChangeUsername(user, command.NewUsername, token);
            if (!result.IsSuccess)
                return result;
            if (rep.Save() == 0)
                return Result.Failure("Invalid save!");
            return Result.Success();
        }
    }
}
