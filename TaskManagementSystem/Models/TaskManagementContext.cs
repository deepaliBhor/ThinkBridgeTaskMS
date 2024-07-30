using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TaskManagementSystem.Models
{
    public partial class TaskManagementContext : DbContext
    {
        public TaskManagementContext()
        {
        }

        public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.NId)
                    .HasName("PK__Document__DF98CDDD3927EBDF");

                entity.Property(e => e.NId).HasColumnName("nId");

                entity.Property(e => e.DtCreatedAt)
                    .HasColumnName("dtCreatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DtUpdatedAt)
                    .HasColumnName("dtUpdatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.NTaskId).HasColumnName("nTaskId");

                entity.Property(e => e.SFilePath)
                    .IsRequired()
                    .HasColumnName("sFilePath")
                    .HasMaxLength(500);

                entity.HasOne(d => d.NTask)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.NTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Document__nTaskI__4316F928");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.NId)
                    .HasName("PK__Employee__DF98CDDD4F0C6E27");

                entity.HasIndex(e => e.SEmail)
                    .HasName("UQ__Employee__07DACB08F9197462")
                    .IsUnique();

                entity.Property(e => e.NId).HasColumnName("nId");

                entity.Property(e => e.NManagerId).HasColumnName("nManagerId");

                entity.Property(e => e.SEmail)
                    .IsRequired()
                    .HasColumnName("sEmail")
                    .HasMaxLength(100);

                entity.Property(e => e.SName)
                    .IsRequired()
                    .HasColumnName("sName")
                    .HasMaxLength(100);

                entity.HasOne(d => d.NManager)
                    .WithMany(p => p.InverseNManager)
                    .HasForeignKey(d => d.NManagerId)
                    .HasConstraintName("FK__Employee__nManag__37A5467C");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.NId)
                    .HasName("PK__Note__DF98CDDD1CD02BEF");

                entity.Property(e => e.NId).HasColumnName("nId");

                entity.Property(e => e.DtCreatedAt)
                    .HasColumnName("dtCreatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DtUpdatedAt)
                    .HasColumnName("dtUpdatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.NTaskId).HasColumnName("nTaskId");

                entity.Property(e => e.SContent)
                    .IsRequired()
                    .HasColumnName("sContent");

                entity.HasOne(d => d.NTask)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.NTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Note__nTaskId__3F466844");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasKey(e => e.NId)
                    .HasName("PK__Task__DF98CDDD33F6D985");

                entity.Property(e => e.NId).HasColumnName("nId");

                entity.Property(e => e.DtCreatedAt)
                    .HasColumnName("dtCreatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DtDueDate)
                    .HasColumnName("dtDueDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DtUpdatedAt)
                    .HasColumnName("dtUpdatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.NEmployeeId).HasColumnName("nEmployeeId");

                entity.Property(e => e.SDescription).HasColumnName("sDescription");

                entity.Property(e => e.SStatus)
                    .IsRequired()
                    .HasColumnName("sStatus")
                    .HasMaxLength(50);

                entity.Property(e => e.STitle)
                    .IsRequired()
                    .HasColumnName("sTitle")
                    .HasMaxLength(200);

                entity.HasOne(d => d.NEmployee)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.NEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Task__nEmployeeI__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
