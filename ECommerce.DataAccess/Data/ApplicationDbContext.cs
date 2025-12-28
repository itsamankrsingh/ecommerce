using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        // This will map to public.category because of the [Table] attribute
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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

                //Foregin key
                entity.Property(e => e.CategoryId)
              .HasColumnName("category_id")
              .IsRequired();

                // FOREIGN KEY MAPPING
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .HasConstraintName("fk_products_categories")
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.ImageUrl)
                     .HasColumnName("image_url");
            });
            #endregion

            #region master.tb_companies
            modelBuilder.Entity<Company>(entity =>
            {
                // Map to exact table name and schema:
                entity.ToTable("tb_companies", "master");

                // If you did NOT use [Table], you can specify here:

                entity.HasKey(e => e.Id);

                // For Postgres identity/serial
                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .UseIdentityByDefaultColumn();

                entity.Property(e => e.Name)
                      .HasColumnName("name")
                      .IsRequired();

                entity.Property(e => e.Address)
                      .HasColumnName("address");

                entity.Property(e => e.City)
                      .HasColumnName("city")
                      .IsRequired();

                entity.Property(e => e.State)
                      .HasColumnName("state");

                entity.Property(e => e.PostalCode)
                      .HasColumnName("postalcode")
                      .IsRequired();

                entity.Property(e => e.PhoneNumber)
                     .HasColumnName("phonenumber")
                     .IsRequired();
            });
            #endregion

            #region master.tb_shoppingcart
            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                // Map to exact table name and schema:
                entity.ToTable("tb_shoppingcart", "master");

                // If you did NOT use [Table], you can specify here:

                entity.HasKey(e => e.Id);

                // For Postgres identity/serial
                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .UseIdentityByDefaultColumn();

                entity.Property(e => e.Count)
                      .HasColumnName("product_count");

                entity.Property(e => e.ApplicationUserId)
                      .HasColumnName("application_user_id")
                      .IsRequired();

                //Foregin key
                entity.Property(e => e.ProductId)
              .HasColumnName("product_id")
              .IsRequired();

                // Correct FOREIGN KEY mapping
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.ShoppingCarts)
                      .HasForeignKey(e => e.ProductId)
                      .HasConstraintName("fk_tb_shoppingcart_tb_products")
                      .OnDelete(DeleteBehavior.Restrict);

            });
            #endregion

            #region master.tb_order_headers
            modelBuilder.Entity<OrderHeader>(entity =>
            {
                // Map to exact table name and schema:
                entity.ToTable("tb_order_headers", "master");

                // If you did NOT use [Table], you can specify here:

                entity.HasKey(e => e.Id);

                // For Postgres identity/serial
                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .UseIdentityByDefaultColumn();

                entity.Property(e => e.ApplicationUserId)
                      .HasColumnName("application_user_id")
                      .IsRequired();

                entity.Property(e => e.OrderDate)
                     .HasColumnName("order_date")
                     .IsRequired();

                entity.Property(e => e.ShippingDate)
                     .HasColumnName("shipping_date");

                entity.Property(e => e.OrderTotal)
                     .HasColumnName("order_total")
                     .IsRequired();

                entity.Property(e => e.OrderStatus)
                     .HasColumnName("order_status");

                entity.Property(e => e.TrackingNumber)
                     .HasColumnName("tracking_number");

                entity.Property(e => e.Carrier)
                     .HasColumnName("carrier");

                entity.Property(e => e.PaymentStatus)
                     .HasColumnName("payment_status");

                entity.Property(e => e.PaymentDate)
                     .HasColumnName("payment_date");

                entity.Property(e => e.PaymentDueDate)
                     .HasColumnName("payment_due_date");

                entity.Property(e => e.PaymentId)
                     .HasColumnName("payment_id");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .IsRequired();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .IsRequired();

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .IsRequired();

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .IsRequired();

                entity.Property(e => e.PostalCode)
                .HasColumnName("postal_code")
                .IsRequired();

                entity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired();
            });
            #endregion

            #region master.tb_order_details
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                // Map to exact table name and schema:
                entity.ToTable("tb_order_details", "master");

                // If you did NOT use [Table], you can specify here:

                entity.HasKey(e => e.Id);

                // For Postgres identity/serial
                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .UseIdentityByDefaultColumn();

                //Foregin key
                entity.Property(e => e.OrderHearderId)
                      .HasColumnName("order_header_id");

                //Foregin key
                entity.Property(e => e.ProductId)
                      .HasColumnName("product_id")
                      .IsRequired();

                entity.Property(e => e.Count)
                     .HasColumnName("count")
                     .IsRequired();

                entity.Property(e => e.Price)
                     .HasColumnName("price")
                     .IsRequired();

                // Correct FOREIGN KEY mapping
                entity.HasOne(e => e.OrderHeader)
                      .WithMany(p => p.OrderDetails)
                      .HasForeignKey(e => e.OrderHearderId)
                      .HasConstraintName("fk_order_details_header")
                      .OnDelete(DeleteBehavior.Restrict);

                // Correct FOREIGN KEY mapping
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.OrderDetails)
                      .HasForeignKey(e => e.ProductId)
                      .HasConstraintName("fk_order_details_product")
                      .OnDelete(DeleteBehavior.Restrict);

            });
            #endregion
        }
    }
}