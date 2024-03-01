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
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(p => p.Questions)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder
                .Entity<User>()
                .HasMany(p => p.Answers)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder
                .Entity<Question>()
                .HasMany(p => p.Answers)
                .WithOne(p => p.Question)
                .HasForeignKey(p => p.QuestionId);

            modelBuilder
                .Entity<Answer>()
                .HasMany(p => p.Votes)
                .WithOne(p => p.Answer)
                .HasForeignKey(p => p.AnswerId);

        }
    }
}
