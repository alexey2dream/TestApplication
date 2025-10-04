using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            IEnumerable<IValidator<TCommand>> validators)
            : ICommandHandler<TCommand>
            where TCommand : ICommand
        {
            public async Task<Result> Handle(TCommand command, CancellationToken token = default)
            {
                var validationResult = await ValidateAsync<TCommand>(command, validators, token);
                if (!validationResult.IsSuccess)
                    return validationResult;
                var result = await innerHandler.Handle(command, token);
                return result;
            }
        }
        public class CommandHandler<TCommand, TResponse>(
            ICommandHandler<TCommand, TResponse> innerHandler,
            IEnumerable<IValidator<TCommand>> validators)
            : ICommandHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken token = default)
            {
                var validationResult = await ValidateAsync<TCommand>(command, validators, token);
                if (!validationResult.IsSuccess)
                    return Result<TResponse>.Failure(validationResult.Error);
                var result = await innerHandler.Handle(command, token);
                return result;
            }
        }
        public class QueryHandler<TQuery, TResponse>(
            IQueryHandler<TQuery, TResponse> innerHandler,
            IEnumerable<IValidator<TQuery>> validators)
            : IQueryHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken token = default)
            {
                var validationResult = await ValidateAsync<TQuery>(query, validators, token);
                if (!validationResult.IsSuccess)
                    return Result<TResponse>.Failure(validationResult.Error);
                var result = await innerHandler.Handle(query, token);
                return result;
            }
        }
        private static async Task<Result<string>> ValidateAsync<TRequest>(
            TRequest request,
            IEnumerable<IValidator<TRequest>> validators,
            CancellationToken token = default)
        {
            if (validators.Count() == 0)
                return Result<string>.Success(null);

            var validationResults = await Task.WhenAll(validators.Select(async v => await v.ValidateAsync(request, token)));
            var validationErrors = validationResults.Where(v => !v.IsValid).SelectMany(v => v.Errors).ToList();
            if (validationErrors.Count > 0)
            {
                string errors = "";
                validationErrors.ForEach(v => errors += $"{v.ErrorMessage} \n");
                return Result<string>.Failure(errors);
            }

            return Result<string>.Success(null);
        }

    }
}
