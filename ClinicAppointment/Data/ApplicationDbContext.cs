using ClinicAppointment.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.AmountPaid)
                .HasColumnType("decimal(18,2)"); // Specifies precision 18 and scale 2

            modelBuilder.Entity<Payment>()
                .Property(p => p.TreatmentCost)
                .HasColumnType("decimal(18,2)"); // Specifies precision 18 and scale 2
        }
        public async Task<List<Payment>> GetAllPayment()
        {
            return await Payments.FromSqlInterpolated($"EXEC GetAllPayments;").ToListAsync();
        }

    }
}
