using Hospital_Management_System_Indu.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System_Indu.DbContextBoth
{
    public class DataConnect : DbContext
    {
        public DataConnect(DbContextOptions<DataConnect> options) : base(options) { }

        public DbSet<IndPerson> IndPerson { get; set; }
        public DbSet<VisitNote> Visits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndPerson>().ToTable("Tbl_Individuals"); // ✅ Correct mapping
            modelBuilder.Entity<IndPerson>().HasKey(p => p.IndID);

            modelBuilder.Entity<VisitNote>().ToTable("Tbl_Visits"); // ✅ Map visit table too
            modelBuilder.Entity<VisitNote>().HasKey(v => v.VisitID);

            modelBuilder.Entity<VisitNote>()
                .HasOne(v => v.Person)
                .WithMany(p => p.VisitNotes)
                .HasForeignKey(v => v.IndID);
        }

    }

}
