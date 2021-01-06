using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyHN.Domain;

namespace MyHN.Infrastructure.EntityTypes
{
    class LinkEntityType : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.ToTable("links");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Url).IsRequired().HasMaxLength(500);
            builder.Property(o => o.CreatedAt).IsRequired();

            builder.OwnsMany(o => o.Votes, vote => {
                vote.ToTable("link_votes");
                vote.WithOwner().HasForeignKey("LinkId");
                vote.HasKey("LinkId", nameof(Vote.Id));
                vote.Property(o => o.Direction).IsRequired();
                vote.Property(o => o.CreatedAt).IsRequired();
            });
        }
    }
}
