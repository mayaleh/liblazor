using Microsoft.EntityFrameworkCore;

namespace MyLibraryOverview.Server.Models
{
    public class UserContext : BaseModel
    {
        //public virtual DbSet<UserAccess> UserAccess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<UserAccess>(entity =>
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
            */
        }
    }
}
