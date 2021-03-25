using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Application.Configuration.Commands;
using Phrases.Domain.Members;

namespace Phrases.Application.Members.Commands.EditMemberGeneralAttributes
{
    public class EditMemberGeneralAttributesCommandHandler : ICommandHandler<EditMemberGeneralAttributesCommand>
    {
        private readonly IMemberRepository _memberRepository;

        public EditMemberGeneralAttributesCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Unit> Handle(EditMemberGeneralAttributesCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetAsync(new MemberId(request.MemberId));

            member.EditGeneralAttributes(request.FirstName, request.LastName, request.Picture);

            return Unit.Value;
        }
    }
}
