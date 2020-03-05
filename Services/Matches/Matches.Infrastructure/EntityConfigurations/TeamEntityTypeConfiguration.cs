using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.TeamAggregate;

namespace Matches.Infrastructure.EntityConfigurations
{
    public class TeamEntityTypeConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("teams", MatchContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(o => o.Id).ForSqlServerUseSequenceHiLo("teamseq", MatchContext.DEFAULT_SCHEMA);



            var navigation = builder.Metadata.FindNavigation(nameof(Team.Players));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<string>("Name").IsRequired();
        }
    }
}
