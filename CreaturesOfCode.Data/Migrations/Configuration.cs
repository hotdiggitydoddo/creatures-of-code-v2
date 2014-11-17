using System.Collections.Generic;
using CreaturesOfCode.Core;
using CreaturesOfCode.Core.Models;

namespace CreaturesOfCode.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CoCDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CoCDbContext context)
        {
            context.Posts.AddOrUpdate(
                p => p.Title,
                new Post
                {
                    Title = "Test Post", 
                    Content = "This is a test post.", 
                    PublishDate = DateTime.UtcNow,
                }
                
            );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
