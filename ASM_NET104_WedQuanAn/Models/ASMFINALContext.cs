using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ASM_NET104_WedQuanAn.Models
{
    public partial class ASMFINALContext : DbContext
    {
        //public ASMFINALContext()
        //{
        //}

        public ASMFINALContext(DbContextOptions<ASMFINALContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Nhom> Nhoms { get; set; }
        public virtual DbSet<ThucDon> ThucDons { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.DiaChiKh)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("DiaChiKH");

                entity.Property(e => e.EmailKh)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EmailKH");

                entity.Property(e => e.Sdtkh)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SDTKH");

                entity.Property(e => e.Tenkh)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("TENKH");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.MaTd });

                entity.ToTable("CartItem");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaTd).HasColumnName("MaTD");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ct_hang");

                entity.HasOne(d => d.MaTdNavigation)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.MaTd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ct_td");
            });

            modelBuilder.Entity<Nhom>(entity =>
            {
                entity.HasKey(e => e.MaNhom);

                entity.ToTable("Nhom");

                entity.Property(e => e.MaNhom).ValueGeneratedNever();

                entity.Property(e => e.TenNhom)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ThucDon>(entity =>
            {
                entity.HasKey(e => e.MaTd);

                entity.ToTable("ThucDon");

                entity.Property(e => e.MaTd).HasColumnName("MaTD");

                entity.Property(e => e.Hinh)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.MoTa).IsRequired();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TenTd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("TenTD");

                entity.HasOne(d => d.NhomNavigation)
                    .WithMany(p => p.ThucDons)
                    .HasForeignKey(d => d.Nhom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ThucDon_nhom");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserEmail);

                entity.ToTable("User");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
