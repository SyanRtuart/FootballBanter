using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates;
using Teams.Infrastructure.Persistence;

namespace Teams.Infrastructure.EntityConfigurations
{
    public class PlayerEntityTypeConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("players", TeamContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(o => o.Id).ForSqlServerUseSequenceHiLo("playerseq", TeamContext.DEFAULT_SCHEMA);



            builder.Property<int>("TeamId").IsRequired();
            
            builder.Property<string>("Name").IsRequired();

            builder.Property<int>("Number").IsRequired();
        }
    }
}
