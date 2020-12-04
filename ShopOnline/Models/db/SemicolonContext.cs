using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


#nullable disable

namespace ShopOnline.Models.db
{
    public partial class SemicolonContext : DbContext
    {
        public SemicolonContext()
        {
        }

        public SemicolonContext(DbContextOptions<SemicolonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\WEBDB;Database=Semicolon;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.CartTotal).HasColumnType("money");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderPayment)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.OrderTotal).HasColumnType("money");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.PdId);

                entity.Property(e => e.PdDes)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PdImage)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PdName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PdPrice).HasColumnType("money");

                entity.Property(e => e.PdType)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PdUpdate).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserBirthday).HasColumnType("datetime");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserFname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("UserFName");

                entity.Property(e => e.UserImage)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserPass)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserUpdate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
