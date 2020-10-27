using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phrases.Domain.Members;
using Phrases.Infrastructure.Persistence;

namespace Phrases.Infrastructure.EntityConfigurations
{
    public class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members", PhraseContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);

            builder.Property<string>("_login").HasColumnName("Login");
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_firstName").HasColumnName("FirstName");
            builder.Property<string>("_lastName").HasColumnName("LastName");
            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<byte[]>("_picture").HasColumnName("Picture");

            builder.OwnsOne<Scores>("_scores", b =>
            {
                b.Property(p => p.Banter).HasColumnName("BanterScore").HasDefaultValue(0);
                b.Property(p => p.Comment).HasColumnName("CommentScore").HasDefaultValue(0);
            });

        }
        
    }
}
