using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaHub.Data
{
    public partial class PizzaDbContext : DbContext
    {
        public PizzaDbContext()
        {
        }

        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CustomerOrder> CustomerOrder { get; set; }
        public virtual DbSet<CustomerOrderPizzaToppings> CustomerOrderPizzaToppings { get; set; }
        public virtual DbSet<CustomerOrderPizzas> CustomerOrderPizzas { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
        public virtual DbSet<RestaurantToppings> RestaurantToppings { get; set; }
        public virtual DbSet<Sizes> Sizes { get; set; }
        public virtual DbSet<Styles> Styles { get; set; }
        public virtual DbSet<Toppings> Toppings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<CustomerOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContactPhoneNumber).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryCity).HasMaxLength(100);

                entity.Property(e => e.DeliveryState).HasMaxLength(100);

                entity.Property(e => e.DeliveryStreet).HasMaxLength(100);

                entity.Property(e => e.DeliveryZipCode).HasMaxLength(20);
            });

            modelBuilder.Entity<CustomerOrderPizzaToppings>(entity =>
            {
                entity.HasKey(e => new { e.LineItemId, e.ToppingId });

                entity.Property(e => e.LineItemId).HasColumnName("LineItemID");

                entity.Property(e => e.ToppingId).HasColumnName("ToppingID");

                entity.Property(e => e.CustomerOrderId).HasColumnName("CustomerOrderID");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.CustomerOrderPizzaToppings)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerOrderPizzaToppings_Toppings");

                entity.HasOne(d => d.CustomerOrderPizzas)
                    .WithMany(p => p.CustomerOrderPizzaToppings)
                    .HasForeignKey(d => new { d.CustomerOrderId, d.LineItemId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerOrderPizzaToppings_CustomerOrderPizzas");
            });

            modelBuilder.Entity<CustomerOrderPizzas>(entity =>
            {
                entity.HasKey(e => new { e.CustomerOrderId, e.LineItemId });

                entity.Property(e => e.CustomerOrderId).HasColumnName("CustomerOrderID");

                entity.Property(e => e.LineItemId)
                    .HasColumnName("LineItemID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.OrderPrice).HasColumnType("money");

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.Property(e => e.StyleId).HasColumnName("StyleID");

                entity.HasOne(d => d.CustomerOrder)
                    .WithMany(p => p.CustomerOrderPizzas)
                    .HasForeignKey(d => d.CustomerOrderId)
                    .HasConstraintName("FK_CustomerOrderPizzas_CustomerORder");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.CustomerOrderPizzas)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_CustomerOrderPizzas_RestaurantID");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.CustomerOrderPizzas)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CustomerOrderPizzas_SizeID");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.CustomerOrderPizzas)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CustomerOrderPizzas_StyleID");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.HasKey(e => new { e.RestaurantId, e.SizeId, e.StyleId });

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.Property(e => e.StyleId).HasColumnName("StyleID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Pizza)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_Pizza_Restaurant");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Pizza)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pizza_Size");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.Pizza)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pizza_Style");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Restaurant)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Restaurant_City");
            });

            modelBuilder.Entity<RestaurantToppings>(entity =>
            {
                entity.HasKey(e => new { e.RestaurantId, e.ToppingId })
                    .HasName("PK_PizzaToppings");

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.ToppingId).HasColumnName("ToppingID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.RestaurantToppings)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_RestaurantToppings_Restaurant");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.RestaurantToppings)
                    .HasForeignKey(d => d.ToppingId)
                    .HasConstraintName("FK_RestaurantToppings_Topping");
            });

            modelBuilder.Entity<Sizes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Styles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Toppings>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });
        }
    }
}
