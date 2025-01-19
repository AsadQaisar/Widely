using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Widely.Models;

namespace Widely.Models
{
    public partial class DB_ATMContext : DbContext
    {
        public DB_ATMContext()
        {
        }

        public DB_ATMContext(DbContextOptions<DB_ATMContext> options)
            : base(options)
        {
        }
        public virtual DbSet<tbl_User> tbl_User { get; set; }
        public virtual DbSet<tbl_Amount> tbl_Amount { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.  
                //                optionsBuilder.UseSqlServer("Server=DESKTOP-GV4424J;Database=TestDB;Trusted_Connection=True;");  

            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_User>(entity =>
            {
                entity.HasKey(e => e.UserID)
                    .HasName("PK__User__7AD04FF1EAFD1DC3");

                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    //.HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    //.HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    //.HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    //.HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DOB)
                    .IsRequired()
                    //.HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LoginPassword)
                    .IsRequired()
                    //.HasMaxLength(255)
                    .IsUnicode(false);

                //entity.Property(e => e.CreatedBy)
                //    .IsRequired()
                //    .HasMaxLength(255)
                //    .IsUnicode(false);

                //entity.Property(e => e.CreatedOn)
                //    .IsRequired()
                //    .HasMaxLength(255)
                //    .IsUnicode(false);

                //entity.Property(e => e.ModifyOn)
                //    .IsRequired()
                //    .HasMaxLength(255)
                //    .IsUnicode(false);

                //entity.Property(e => e.ModifyBy)
                //    .IsRequired()
                //    .HasMaxLength(255)
                //    .IsUnicode(false);

                //entity.Property(e => e.UserRoleID)
                //   .IsRequired()
                //   .HasMaxLength(255)
                //   .IsUnicode(false);

                //entity.Property(e => e.IsActive)
                //    .IsRequired()
                //    .HasMaxLength(255)
                //    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


