using System;
using Matches.Domain.Aggregates.Match;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matches.Infrastructure.EntityConfigurations
{
    public class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("matches", MatchContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(o => o.Id).ForSqlServerUseSequenceHiLo("matchseq", MatchContext.DEFAULT_SCHEMA);

            builder.Property<int>("_homeTeamId").HasColumnName("HomeTeamId");
            builder.Property<int>("_awayTeamId").HasColumnName("AwayTeamId");
            builder.Property<int>("_statusId").HasColumnName("StatusId");
            builder.Property<DateTime>("_utcDate").HasColumnName("UtcDate");

            builder.OwnsOne<Score>("_score", b =>
            {
                b.Property(p => p.Winner).HasColumnName("ScoreWinner");
                b.Property(p => p.HomeTeam).HasColumnName("ScoreHomeTeam");
                b.Property(p => p.AwayTeam).HasColumnName("ScoreAwayTeam");
            });
        }
    }
}