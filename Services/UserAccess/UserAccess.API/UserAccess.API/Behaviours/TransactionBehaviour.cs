using System;
using System.Threading;
using System.Threading.Tasks;
using Base.Infrastructure.DomainEventsDispatching;
using Base.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.API.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly UserAccessContext _dbContext;
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public TransactionBehaviour(UserAccessContext dbContext,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger, 
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(UserAccessContext));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                if (_dbContext.HasActiveTransaction) return await next();

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    using (var transaction = await _dbContext.BeginTransactionAsync())
                    using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})",
                            transaction.TransactionId, typeName, request);

                        response = await next();
                            
                        _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}",
                            transaction.TransactionId, typeName);

                        await _dbContext.CommitTransactionAsync(transaction);
                        await _domainEventsDispatcher.DispatchEventsAsync();

                        transactionId = transaction.TransactionId;
                    }

                    //await _orderingIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                });
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

                throw;
            }
        }
    }
}