using ECommerce.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Web.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        // This will map to public.category because of the [Table] attribute
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                // Map to exact table name and schema:
                entity.ToTable("Tb_Categories", "master");

                // If you did NOT use [Table], you can specify here:
                // entity.ToTable("category", "public");

                entity.HasKey(e => e.Id);

                // For Postgres identity/serial
                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .UseIdentityByDefaultColumn();

                entity.Property(e => e.Name)
                      .HasColumnName("name")
                      .IsRequired();

                entity.Property(e => e.DisplayOrder)
                      .HasColumnName("display_order");
            });
        }
    }
}
