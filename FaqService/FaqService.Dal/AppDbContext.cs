using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaqService.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Runtime.InteropServices;

namespace FaqService.Dal
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(p => p.Questions)
                .WithOne(p => p.User!)
                .HasForeignKey(p => p.UserId);
            modelBuilder
                .Entity<Question>()
                .HasOne(p => p.User)
                .WithMany(p => p.Questions)
                .HasForeignKey(p => p.UserId);

            modelBuilder
               .Entity<Question>()
               .HasMany(p => p.Questions)
               .WithOne(p => p.User!)
               .HasForeignKey(p => p.UserId);
            modelBuilder
                .Entity<Question>()
                .HasOne(p => p.User)
                .WithMany(p => p.Questions)
                .HasForeignKey(p => p.UserId);

            //modelBuilder
            //    .Entity<Platform>()
            //    .HasMany(p => p.Commands)
            //    .WithOne(p => p.Platform!)
            //    .HasForeignKey(p => p.PlatformId);

            //modelBuilder
            //    .Entity<Command>()
            //    .HasOne(p => p.Platform)
            //    .WithMany(p => p.Commands)
            //    .HasForeignKey(p => p.PlatformId);

        }
    }
}
