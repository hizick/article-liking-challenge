using LikeButton.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeButton.Persistence
{
    public class LikeDbContext : DbContext
    {
        public LikeDbContext(DbContextOptions<LikeDbContext> options)
            : base(options){}
        public DbSet<Like> Like { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Article> Article { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.UserId });
                //this prevents insertion of duplicate records
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new {e.UserId });
            });
        }
    }
}
