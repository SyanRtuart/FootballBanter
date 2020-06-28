using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phrases.Domain.Phrase;
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


            builder.Property<Guid>("_matchId").HasColumnName("MatchId").IsRequired();

            builder.Property<Guid>("_teamId").HasColumnName("TeamId").IsRequired();

            builder.Property<string>("_description").HasColumnName("Description").IsRequired();

            builder.Property<bool>("_positive").HasColumnName("Positive").IsRequired();

            builder.Property<DateTime>("_dateCreated").HasColumnName("DateCreated").IsRequired();

            builder.Property<bool>("_isDeleted").HasColumnName("IsDeleted");

            builder.Property<DateTime>("_dateDeleted").HasColumnName("DateDeleted");

            builder.Property<int>("_score").HasColumnName("Score").IsRequired();
        }
    }
}