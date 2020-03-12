using FluentValidation;

namespace Phrases.Application.Phrases.Commands.CreatePhrase
{
    public class CreatePhraseCommandValidator : AbstractValidator<CreatePhraseCommand>
    {
        public CreatePhraseCommandValidator()
        {
            RuleFor(x => x.TeamId).Must(i => i.Equals(100)).WithMessage("ID must be 100");
        }
    }
}