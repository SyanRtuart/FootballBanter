using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain.Users;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.Infrastructure.EntityConfigurations
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", UserAccessContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);

            builder.Property<string>("_login").HasColumnName("Login");
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_password").HasColumnName("Password");
            builder.Property<bool>("_isActive").HasColumnName("IsActive");
            builder.Property<string>("_firstName").HasColumnName("FirstName");
            builder.Property<string>("_lastName").HasColumnName("LastName");
            builder.Property<string>("_name").HasColumnName("Name");

            builder.OwnsOne<Scores>("_scores", b =>
            {
                b.Property(p => p.Banter).HasColumnName("BanterScore").HasDefaultValue(0);
                b.Property(p => p.Comment).HasColumnName("CommentScore").HasDefaultValue(0);
            });

            builder.OwnsMany<UserRole>("_roles", b =>
            {
                b.WithOwner().HasForeignKey("UserId");
                b.ToTable("UserRoles", UserAccessContext.DEFAULT_SCHEMA);
                b.Property<Guid>("UserId");
                b.Property<string>("Value").HasColumnName("RoleCode");
                b.HasKey("UserId", "Value");
            });

            builder.Property<byte[]>("_picture").HasColumnName("Picture");
        }
    }
}