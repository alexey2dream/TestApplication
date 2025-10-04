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
    public static class LoggingBehaviorDecorator
    {
        public class CommandHandler<TCommand>(
            ICommandHandler<TCommand> innerHandler,
            ILogger<CommandHandler<TCommand>> logger)
            : ICommandHandler<TCommand>
            where TCommand : ICommand
        {
            public async Task<Result> Handle(TCommand command, CancellationToken token = default)
            {
                string requestName = typeof(TCommand).Name;
                logger.LogInformation($"Starting request: {requestName}");
                var result = await innerHandler.Handle(command, token);
                if (!result.IsSuccess)
                    logger.LogError($"Request ended with error: {requestName}");
                else
                    logger.LogError($"Request completed: {requestName}");
                return result;
            }
        }
        public class CommandHandler<TCommand, TResponse>(
            ICommandHandler<TCommand, TResponse> innerHandler,
            ILogger<CommandHandler<TCommand, TResponse>> logger)
            : ICommandHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken token = default)
            {
                string requestName = typeof(TCommand).Name;
                logger.LogInformation($"Starting request: {requestName}");
                var result = await innerHandler.Handle(command, token);
                if (!result.IsSuccess)
                    logger.LogError($"Request ended with error: {requestName}");
                else
                    logger.LogError($"Request completed: {requestName}");
                return result;
            }
        }
        public class QueryHandler<TQuery, TResponse>(
            IQueryHandler<TQuery, TResponse> innerHandler,
            ILogger<QueryHandler<TQuery, TResponse>> logger)
            : IQueryHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken token = default)
            {
                string requestName = typeof(TQuery).Name;
                logger.LogInformation($"Starting request: {requestName}");
                var result = await innerHandler.Handle(query, token);
                if (!result.IsSuccess)
                    logger.LogError($"Request ended with error: {requestName}");
                else
                    logger.LogError($"Request completed: {requestName}");
                return result;
            }
        }

    }
}
