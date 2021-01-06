using Microsoft.EntityFrameworkCore;
using MyHN.Application;
using MyHN.Domain;
using System.Linq;

namespace MyHN.Infrastructure
{
    public class MyHNDbContext : DbContext, IContext
    {
        public DbSet<Link> Links { get; set; }
        public DbSet<Comment> Comments { get; set; }

        IQueryable<Link> IContext.Links => Links;
        IQueryable<Comment> IContext.Comments => Comments;

        public MyHNDbContext()
        {

        }

        public MyHNDbContext(DbContextOptions<MyHNDbContext> options) : base(options) 
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new LinkEntityType());


            /*   modelBuilder.Entity<Link>()
                   .Property(o => o.Url).IsRequired().HasMaxLength(255);*/

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=:memory:");
            }
            //base.OnConfiguring(optionsBuilder);

            //optionsBuilder.UseSqlite("Data Source=:memory:");
        }
    }
}
