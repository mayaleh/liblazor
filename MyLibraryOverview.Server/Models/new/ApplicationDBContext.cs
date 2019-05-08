using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLibraryOverview.Server.Models.Entities;
//using MyLibraryOverview.Shared;

namespace MyLibraryOverview.Server.Models
{
    public class ApplicationDBContext : IdentityDbContext<UserAppIdentity>
    {

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<UserBook> UserBook { get; set; }


        public ApplicationDBContext()
        {

        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<UserAppIdentity>(u =>
            {
                // User has many books
                u.HasMany<UserBook>()
                 .WithOne()
                 .HasForeignKey(ub => ub.UserAppIdentityId)
                 .IsRequired();
            });
        } */
    }
}
