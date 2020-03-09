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
    public class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("matches", MatchContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(o => o.Id).ForSqlServerUseSequenceHiLo("matchseq", MatchContext.DEFAULT_SCHEMA);


            builder.HasOne<Team>()
                .WithMany()
                .HasForeignKey("_homeTeamId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Team>()
                .WithMany()
                .HasForeignKey("_awayTeamId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property<DateTime>("_utcDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("UtcDate")
                .IsRequired();

            builder
                .Property<int>("_matchStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("MatchStatusId")
                .IsRequired();


            builder
                .OwnsOne(o => o.Score, a =>
                {
                    a.WithOwner();
                });

            
        }
    }
}
