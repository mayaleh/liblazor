using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Shared;

namespace PersonalLibrary.Server.Models
{
    public class BaseModel : DbContext
    {

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<UserAccess> UserAccess { get; set; }

        public BaseModel()
        {
        }

        public BaseModel(DbContextOptions<BaseModel> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=localhost:4112;Database=liblazor;Username=appuser;Password=libraryUser1997");
            
            if (!optionsBuilder.IsConfigured)
            {
                //"User ID=appuser;Password=libraryUser1997;Server=localhost;Port=4112;Database=liblazor"
                //
                optionsBuilder.UseNpgsql("User ID=appuser;Password=libraryUser1997;Server=localhost;Port=4112;Database=liblazor");
                // optionsBuilder.UseSqlServer(@"User ID=appuser;Password=libraryUser1997;Host=localhost;Port=5432;Database=liblazor;");
                //:4112
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
