using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Shared;

namespace PersonalLibrary.Server.Models
{
    public class AuthorContext : BaseModel
    {
        //public virtual DbSet<Author> Author { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.ForNpgsqlHasComment("Authors Writters o books");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.About)
                    .HasColumnName("about")
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150);
            });
        }
    }
}
