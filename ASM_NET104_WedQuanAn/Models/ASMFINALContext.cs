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

        public virtual DbSet<CartDetail> CartDetails { get; set; }
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

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CartDetail");

                entity.Property(e => e.Sl).HasColumnName("SL");
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
