using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matches.Infrastructure.EntityConfigurations
{
    public class TeamEntityTypeConfiguration : IEntityTypeConfiguration<Teams.Domain.Aggregates.Team>
    {
        public void Configure(EntityTypeBuilder<Teams.Domain.Aggregates.Team> builder)
        {
            builder.ToTable("teams", TeamContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(o => o.Id).ForSqlServerUseSequenceHiLo("teamseq", TeamContext.DEFAULT_SCHEMA);



            var navigation = builder.Metadata.FindNavigation(nameof(Teams.Domain.Aggregates.Team.Players));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<string>("Name").IsRequired();
        }
    }
}
