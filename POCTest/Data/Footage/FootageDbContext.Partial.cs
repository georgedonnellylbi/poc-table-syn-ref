using Microsoft.EntityFrameworkCore;
using POCTest.Models.Footage;
using POCTest.Models.Common;

namespace POCTest.Data.Footage
{
    public partial class FootageDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // Map Club entity to synonym table in Footage DB
            modelBuilder.Entity<POCTest.Models.Common.Club>().ToTable("Clubs", "dbo");

            // Set up navigation property for Film, specify foreign key explicitly
            modelBuilder.Entity<Film>(eb =>
            {
                    eb.HasOne(f => f.Club)
                        .WithMany()
                        .HasForeignKey(f => f.ClubId)
                        .HasPrincipalKey(c => c.ClubId)
                        .OnDelete(DeleteBehavior.ClientNoAction);
            });
        }
    }
}
