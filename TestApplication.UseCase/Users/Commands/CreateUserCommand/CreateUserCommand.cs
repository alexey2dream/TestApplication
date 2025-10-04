using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Users.Commands.CreateUserCommand
{
    public class CreateUserCommand : ICommand
    {
    }
    public class CreateUserCommandHandler(
        IUserRep rep
        )
        : ICommandHandler<CreateUserCommand>
    {
        public Task<Result> Handle(CreateUserCommand command, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
