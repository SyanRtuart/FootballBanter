using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Infrastructure.Inbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.Infrastructure.EntityConfigurations
{
    public class InboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<InboxMessage>
    {
        public void Configure(EntityTypeBuilder<InboxMessage> builder)
        {
            builder.ToTable("InboxMessages", UserAccessContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedNever();
        }
    }
}
