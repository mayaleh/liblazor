using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonalLibrary.Server
{
    public partial class liblazorContext : DbContext
    {
        public liblazorContext()
        {
        }

        public liblazorContext(DbContextOptions<liblazorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Useraccess> Useraccess { get; set; }
        public virtual DbSet<Userbook> Userbook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=4112;Database=liblazor;Username=appuser;Password=libraryUser1997");
            }
        }

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

            modelBuilder.Entity<Useraccess>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("useraccess");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(250);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Userbook>(entity =>
            {
                entity.HasKey(e => e.Userbook1);

                entity.ToTable("userbook");

                entity.ForNpgsqlHasComment("Users Books table");

                entity.Property(e => e.Userbook1).HasColumnName("userbook");

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
        }
    }
}
