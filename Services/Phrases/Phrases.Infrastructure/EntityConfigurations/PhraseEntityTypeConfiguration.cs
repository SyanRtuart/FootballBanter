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

            builder.Property<string>("Description").IsRequired();

            builder.Property<int>("MatchId").IsRequired();
            
            builder.Property<int>("TeamId").IsRequired();
            
            builder.Property<bool>("Positive").IsRequired();
        }
    }
}
