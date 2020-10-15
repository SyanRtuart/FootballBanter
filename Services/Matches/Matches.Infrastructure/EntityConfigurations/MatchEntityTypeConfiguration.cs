using System;
using Matches.Domain.Match;
using Matches.Domain.Team;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matches.Infrastructure.EntityConfigurations
{
    public class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches", MatchContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property<TeamId>("_homeTeamId").HasColumnName("HomeTeamId").IsRequired();
            builder.Property<TeamId>("_awayTeamId").HasColumnName("AwayTeamId").IsRequired();
            builder.Property<string>("_status").HasColumnName("Status").IsRequired();
            builder.Property<DateTime>("_utcDate").HasColumnName("UtcDate").IsRequired();
            builder.Property<string>("_name").HasColumnName("Name").IsRequired();
            builder.Property<string>("_season").HasColumnName("Season").IsRequired();
            builder.Property<string>("_externalId").HasColumnName("ExternalId").IsRequired();


            builder.OwnsOne<Score>("_score", b =>
            {
                b.Property(p => p.Winner).HasColumnName("ScoreWinner");
                b.Property(p => p.HomeTeam).HasColumnName("ScoreHomeTeam");
                b.Property(p => p.AwayTeam).HasColumnName("ScoreAwayTeam");
            });
        }
    }
}