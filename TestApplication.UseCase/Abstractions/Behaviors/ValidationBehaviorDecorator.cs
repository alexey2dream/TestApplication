using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Abstractions.Behaviors
{
    public static class ValidationBehaviorDecorator
    {
        public class CommandHandler<TCommand>(
            ICommandHandler<TCommand> innerHandler,
            IValidator<TCommand> validator)
            : ICommandHandler<TCommand>
            where TCommand : ICommand
        {
            public async Task<Result> Handle(TCommand command, CancellationToken token = default)
            {
                var validationResult = await validator.ValidateAsync(command, token);
                if (!validationResult.IsValid)
                {
                    string errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                    return Result.Failure(errors);
                }

                var result = await innerHandler.Handle(command, token);
                return result;
            }
        }
        public class CommandHandler<TCommand, TResponse>(
            ICommandHandler<TCommand, TResponse> innerHandler,
            IValidator<TCommand> validator)
            : ICommandHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken token = default)
            {
                //if (validator != null)
                //{
                    var validationResult = await validator.ValidateAsync(command, token);
                    if (!validationResult.IsValid)
                    {
                        string errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                        return Result<TResponse>.Failure(errors);
                    }
                //}

                var result = await innerHandler.Handle(command, token);
                return result;
            }
        }
        public class QueryHandler<TQuery, TResponse>(
            IQueryHandler<TQuery, TResponse> innerHandler,
            IValidator<TQuery> validator)
            : IQueryHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken token = default)
            {
                var validationResult = await validator.ValidateAsync(query, token);
                if (!validationResult.IsValid)
                {
                    string errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                    return Result<TResponse>.Failure(errors);
                }

                var result = await innerHandler.Handle(query, token);
                return result;
            }
        }

    }
}
