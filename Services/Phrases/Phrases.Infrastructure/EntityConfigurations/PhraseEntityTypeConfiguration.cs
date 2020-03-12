using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phrases.Domain.Aggregates.PhraseAggregate;
using Phrases.Infrastructure.Persistence;

namespace Phrases.Infrastructure.EntityConfigurations
{
    public class PhraseEntityTypeConfiguration : IEntityTypeConfiguration<Phrase>
    {
        public void Configure(EntityTypeBuilder<Phrase> builder)
        {
            builder.ToTable("phrases", PhraseContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(o => o.Id).ForSqlServerUseSequenceHiLo("phraseseq", PhraseContext.DEFAULT_SCHEMA);


            builder.Property<int>("_matchId").HasColumnName("MatchId").IsRequired();
            
            builder.Property<int>("_teamId").HasColumnName("TeamId").IsRequired();

            builder.Property<string>("_description").HasColumnName("Description").IsRequired();

            builder.Property<bool>("_positive").HasColumnName("Positive").IsRequired();

            builder.Property<DateTime>("_dateCreated").HasColumnName("DateCreated").IsRequired();
        }
    }
}
