using PersonalLibrary.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using PersonalLibrary.Shared;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PersonalLibrary.Server.Models
{
    public class BookContext : BaseModel
    {
        //public virtual DbSet<Book> Book { get; set; }
        //public virtual DbSet<Author> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.
            /*
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


            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.ForNpgsqlHasComment("Books table");

                entity.Property(e => e.Bookid).HasColumnName("bookid");

                entity.Property(e => e.About)
                    .HasColumnName("about")
                    .HasMaxLength(500);

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.Authorid)
                    .HasConstraintName("book_author_authorid_fk");
            });



            modelBuilder.Entity<Userbook>(entity =>
            {
                entity.HasKey(e => e.Userbookid);

                entity.ToTable("userbook");

                entity.ForNpgsqlHasComment("Users Books table");

                entity.Property(e => e.Userbookid).HasColumnName("userbook");

                entity.Property(e => e.Bookid).HasColumnName("bookid");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(500);

                entity.Property(e => e.Place)
                    .HasColumnName("place")
                    .HasMaxLength(150);

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.Readdone).HasColumnName("readdone");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Userbook)
                    .HasForeignKey(d => d.Bookid)
                    .HasConstraintName("userbook_book_bookid_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userbook)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("userbook_useraccess_userid_fk");
            });
            */
        }
    }

   
}
