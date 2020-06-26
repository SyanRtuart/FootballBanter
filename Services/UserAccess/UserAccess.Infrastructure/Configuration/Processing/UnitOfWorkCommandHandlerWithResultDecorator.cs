﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Domain.SeedWork;
using UserAccess.Application.Configuration.Commands;
using UserAccess.Application.Contracts;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult> where T : ICommand<TResult>
    {
        private readonly ICommandHandler<T, TResult> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserAccessContext _userAccessContext;

        public UnitOfWorkCommandHandlerWithResultDecorator(
            ICommandHandler<T, TResult> decorated,
            IUnitOfWork unitOfWork,
            UserAccessContext userAccessContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _userAccessContext = userAccessContext;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            var result = await this._decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase<TResult>)
            {
                //var internalCommand = await _userAccessContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

                //if (internalCommand != null)
                //{
                //    internalCommand.ProcessedDate = DateTime.UtcNow;
                //}
            }

            await this._unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}
