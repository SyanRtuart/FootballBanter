﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Domain.Phrase;

namespace Phrases.Application.Phrases.Commands.DeletePhrase
{
    public class DeletePhraseCommandHandler : IRequestHandler<DeletePhraseCommand, Unit>
    {
        private readonly IPhraseRepository _phraseRepository;

        public DeletePhraseCommandHandler(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<Unit> Handle(DeletePhraseCommand request, CancellationToken cancellationToken)
        {
            var phrase = await _phraseRepository.GetAsync(request.PhraseId);

            phrase.Delete();

            return Unit.Value;
        }
    }
}