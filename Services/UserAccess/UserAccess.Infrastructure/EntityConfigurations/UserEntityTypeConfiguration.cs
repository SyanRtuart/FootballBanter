﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain.Users;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
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

            builder.OwnsMany<UserRole>("_roles", b =>
            {
                b.WithOwner().HasForeignKey("UserId");
                b.ToTable("UserRoles", UserAccessContext.DEFAULT_SCHEMA);
                b.Property<Guid>("UserId");
                b.Property<string>("Value").HasColumnName("RoleCode");
                b.HasKey("UserId", "Value");
            });
        }
    }
}