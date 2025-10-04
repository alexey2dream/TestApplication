using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Repositories;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Users.Commands.BanUserCommand
{
    public class BanUserCommand : ICommand
    {
        public int Id { get; set; }
    }
    public class BanUserCommandHandler(
        IUserRep rep)
        : ICommandHandler<BanUserCommand>
    {
        public async Task<Result> Handle(BanUserCommand command, CancellationToken token = default)
        {
            var user = await rep.GetById(command.Id, token);
            if (user is null)
                return Result.Failure("User not exists!");
            if(!await rep.Delete(user, token))
                return Result.Failure("Invalid delete!");
            if(rep.Save() == 0)
                return Result.Failure("Invalid save!");
            return Result.Success();
        }
    }
}
