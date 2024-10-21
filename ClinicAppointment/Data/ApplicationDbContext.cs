using ClinicAppointment.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

        public DbSet<Appointment> Appointments { get; set; }

    }
}
