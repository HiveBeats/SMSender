using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SMSender.Shared.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            var converter = new ValueConverter<IEnumerable<string>, string>(
                from => string.Join(';', from),
                to => to.Split(";", StringSplitOptions.None)
            );
            
            modelBuilder.Entity<ShortMessage>(entity => entity.Property(m => m.Id)
                .HasMaxLength(36));
            modelBuilder.Entity<ShortMessage>(entity => entity.Property(m => m.From)
                .HasMaxLength(32));
            modelBuilder.Entity<ShortMessage>(entity => entity.Property(m => m.To)
                .HasConversion(converter));
            modelBuilder.Entity<ShortMessage>(entity => entity.Property(m => m.Status)
                .HasConversion<string>()
                .HasMaxLength(32));
        }

        public DbSet<ShortMessage> ShortMessages { get; set; }
    }
}