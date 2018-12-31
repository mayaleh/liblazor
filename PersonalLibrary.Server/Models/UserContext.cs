using PersonalLibrary.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using PersonalLibrary.Shared;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PersonalLibrary.Server.Models
{
    public class UserContext : BaseModel
    {
        //public virtual DbSet<UserAccess> UserAccess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccess>(entity =>
            {
                entity.ToTable("useraccess");

                entity.ForNpgsqlHasComment("Users login");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(250);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(500);


                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(250);
            });
        }
    }
}
