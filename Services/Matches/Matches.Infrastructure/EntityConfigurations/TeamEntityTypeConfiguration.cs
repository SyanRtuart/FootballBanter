using Matches.Domain.Team;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matches.Infrastructure.EntityConfigurations
{
    public class TeamEntityTypeConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams", MatchContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property<string>("_name").HasColumnName("Name").IsRequired();
            builder.Property<string>("_description").HasColumnName("Description");
            builder.Property<byte[]>("_logo").HasColumnName("Logo");
            builder.Property<string>("_manager").HasColumnName("Manager");
            builder.Property<string>("_league").HasColumnName("League");
            builder.Property<string>("_country").HasColumnName("Country");
            builder.Property<int>("_formedYear").HasColumnName("FormedYear");
            builder.Property<string>("_facebook").HasColumnName("Facebook");
            builder.Property<string>("_instagram").HasColumnName("Instagram");
            builder.Property<string>("_externalId").HasColumnName("ExternalId").IsRequired();


            builder.OwnsOne<Stadium>("_stadium", b =>
            {
                b.Property(p => p.Name).HasColumnName("StadiumName");
                b.Property(p => p.Description).HasColumnName("StadiumDescription");
                b.Property(p => p.Location).HasColumnName("StadiumLocation");
            });

        }
    }
}