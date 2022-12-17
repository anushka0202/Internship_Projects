using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectApprovalRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApprovalRepo.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //creating a representation of our Project Model in our database using Dbset as a prop
        public DbSet<Project> Projects { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserProjectMapping> UserProjectMappings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)  //we use the base keyword to call the constructor from DbContext and then we pass in the options
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //for AppUserProject entity we are defining a combination of primary keys
            builder.Entity<UserProjectMapping>()
                .HasKey(x => new { x.AppUserId, x.ProjectId });

            builder.Entity<UserProjectMapping>().HasOne(p => p.Project).WithMany(x => x.UserProjectMappings).HasForeignKey(p => p.ProjectId);
            builder.Entity<UserProjectMapping>().HasOne(u => u.AppUser).WithMany(x => x.UserProjectMappings).HasForeignKey(u => u.AppUserId);
        }

    }
}
