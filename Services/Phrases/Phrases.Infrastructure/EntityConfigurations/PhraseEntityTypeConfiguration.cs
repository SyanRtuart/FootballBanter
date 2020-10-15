using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phrases.Domain.Match;
using Phrases.Domain.Phrase;
using Phrases.Domain.Team;
using Phrases.Domain.User;
using Phrases.Infrastructure.Persistence;

namespace Phrases.Infrastructure.EntityConfigurations
{
    public class PhraseEntityTypeConfiguration : IEntityTypeConfiguration<Phrase>
    {
        public void Configure(EntityTypeBuilder<Phrase> builder)
        {
            builder.ToTable("Phrases", PhraseContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property<MatchId>("_matchId").HasColumnName("MatchId").IsRequired();

            builder.Property<TeamId>("_teamId").HasColumnName("TeamId").IsRequired();

            builder.Property<UserId>("_createdByUserId").HasColumnName("CreatedByUserId").IsRequired();

            builder.Property<string>("_description").HasColumnName("Description").IsRequired();

            builder.Property<bool>("_positive").HasColumnName("Positive").IsRequired();

            builder.Property<DateTime>("_dateCreated").HasColumnName("DateCreated").IsRequired();

            builder.Property<UserId?>("_deletedByUserId").HasColumnName("DeletedByUserId");

            builder.Property<DateTime?>("_dateDeleted").HasColumnName("DateDeleted");

            builder.Property<int>("_score").HasColumnName("Score").IsRequired();

            builder.OwnsMany<PhraseVoteHistory>("_phraseVoteHistory", y =>
            {
                y.WithOwner().HasForeignKey("PhraseId");
                y.ToTable("PhraseVoteHistory", PhraseContext.DEFAULT_SCHEMA);
                y.HasKey(o => o.Id);
                y.Property<UserId>("UserId");
                y.Property<int>("_score").HasColumnName("Score");
                y.Property<DateTime>("_utcDateVoted").HasColumnName("DateVoted");
                y.Property<DateTime>("_utcDateDeleted").HasColumnName("DateDeleted");
            });
        }
    }
}