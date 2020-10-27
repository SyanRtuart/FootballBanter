using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Phrases.Application.Configuration.Commands;
using Phrases.Domain.Members;

namespace Phrases.Application.Members.Commands.CreateMember
{
    public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;

        public CreateMemberCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Unit> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = Member.Create(request.MemberId, request.Email, request.Login, request.Email, request.FirstName,
                request.LastName, request.Name);

            await _memberRepository.AddAsync(member);

            return Unit.Value;
        }
    }
}
