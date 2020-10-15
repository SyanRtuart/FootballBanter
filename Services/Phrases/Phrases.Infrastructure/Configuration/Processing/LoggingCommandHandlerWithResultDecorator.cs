﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Base.Application.BuildingBlocks;
using Microsoft.EntityFrameworkCore;
using Phrases.Application.Configuration.Commands;
using Phrases.Application.Contracts;
using Phrases.Domain.Phrase;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace Phrases.Infrastructure.Configuration.Processing
{
    internal class LoggingCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult>
        where T : ICommand<TResult>
    {
        private readonly ICommandHandler<T, TResult> _decorated;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILogger _logger;

        public LoggingCommandHandlerWithResultDecorator(
            ILogger logger,
            IExecutionContextAccessor executionContextAccessor,
            ICommandHandler<T, TResult> decorated)
        {
            _logger = logger;
            _executionContextAccessor = executionContextAccessor;
            _decorated = decorated;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            using (
                LogContext.Push(
                    new RequestLogEnricher(_executionContextAccessor),
                    new CommandLogEnricher(command)))
            {
                try
                {
                    _logger.Information(
                        "Executing command {@Command}",
                        command);

                    var result = await _decorated.Handle(command, cancellationToken);

                    _logger.Information("Command processed successful, result {Result}", result);

                    return result;
                }
                catch (Exception exception)
                {
                    _logger.Error(exception, "Command processing failed");
                    throw;
                }
            }
        }

        private class CommandLogEnricher : ILogEventEnricher
        {
            private readonly ICommand<TResult> _command;

            public CommandLogEnricher(ICommand<TResult> command)
            {
                _command = command;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty("Context",
                    new ScalarValue($"Command:{_command.Id.ToString()}")));
            }
        }

        private class RequestLogEnricher : ILogEventEnricher
        {
            private readonly IExecutionContextAccessor _executionContextAccessor;

            public RequestLogEnricher(IExecutionContextAccessor executionContextAccessor)
            {
                _executionContextAccessor = executionContextAccessor;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                if (_executionContextAccessor.IsAvailable)
                    logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId",
                        new ScalarValue(_executionContextAccessor.CorrelationId)));
            }
        }
    }
}