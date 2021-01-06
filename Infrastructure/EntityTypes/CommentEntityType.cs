using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyHN.Domain;

namespace MyHN.Infrastructure.EntityTypes
{
    public class CommentEntityType : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comments");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Content).IsRequired();
            builder.Property(o => o.CreatedAt).IsRequired();
            builder.HasOne<Link>()
                .WithMany()
                .HasForeignKey(o => o.LinkId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
