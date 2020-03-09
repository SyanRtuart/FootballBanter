using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.MatchAggregate;

namespace Matches.Infrastructure.EntityConfigurations
{
    public class MatchStatusEntityTypeConfiguration : IEntityTypeConfiguration<MatchStatus>
    {
        public void Configure(EntityTypeBuilder<MatchStatus> builder)
        {
            builder.ToTable("matchstatus", MatchContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
