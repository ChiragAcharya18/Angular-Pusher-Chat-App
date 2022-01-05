using System;
using ChatDemo.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ChatDemo.Model
{
    public partial class ChatAppDemoContext : DbContext
    {
        public ChatAppDemoContext()
        {
        }

        public ChatAppDemoContext(DbContextOptions<ChatAppDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChatBackup> ChatBackup { get; set; }
        public virtual DbSet<GetAllMessages> ChatsSp { get; set; }
        public virtual DbSet<UserModal> UserDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAP00282\\SQLEXPRESS17;Database=ChatAppDemo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatBackup>(entity =>
            {
                entity.Property(e => e.ChannelName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EventName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .IsUnicode(false);

                entity.Property(e => e.TextDateTime).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
