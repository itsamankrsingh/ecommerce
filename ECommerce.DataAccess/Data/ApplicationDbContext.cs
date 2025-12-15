using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        // This will map to public.category because of the [Table] attribute
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region master.tb_categories
            modelBuilder.Entity<Category>(entity =>
            {
                // Map to exact table name and schema:
                entity.ToTable("tb_categories", "master");

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
            #endregion

            #region master.tb_products
            modelBuilder.Entity<Product>(entity =>
            {
                // Map to exact table name and schema:
                entity.ToTable("tb_products", "master");

                // If you did NOT use [Table], you can specify here:
                // entity.ToTable("category", "public");

                entity.HasKey(e => e.Id);

                // For Postgres identity/serial
                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .UseIdentityByDefaultColumn();

                entity.Property(e => e.Title)
                      .HasColumnName("title")
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasColumnName("description");

                entity.Property(e => e.ISBN)
                      .HasColumnName("isbn")
                      .IsRequired();

                entity.Property(e => e.Author)
                      .HasColumnName("author")
                      .IsRequired();

                entity.Property(e => e.ListPrice)
                      .HasColumnName("list_price")
                      .IsRequired();

                entity.Property(e => e.Price)
                     .HasColumnName("price")
                     .IsRequired();

                entity.Property(e => e.Price50)
                     .HasColumnName("price50")
                     .IsRequired();

                entity.Property(e => e.Price100)
                     .HasColumnName("price100")
                     .IsRequired();

                // UNIQUE constraint mapping
                entity.HasIndex(e => e.ISBN)
                      .IsUnique()
                      .HasDatabaseName("uq_books_isbn");
            });
            #endregion
        }
    }
}
