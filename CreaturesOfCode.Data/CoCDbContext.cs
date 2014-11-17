using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaturesOfCode.Core;
using CreaturesOfCode.Core.Models;

namespace CreaturesOfCode.Data
{
    public class CoCDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>()
                .HasMany(t => t.Posts)
                .WithMany(t => t.Tags)
                .Map(m =>
                {
                    m.ToTable("PostTags");
                    m.MapLeftKey("PostId");
                    m.MapRightKey("TagId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
