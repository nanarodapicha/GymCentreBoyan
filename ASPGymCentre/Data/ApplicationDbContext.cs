using ASPGymCentre.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPGymCentre.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Reservation> Reservations { get; set; }
        // public DbSet<Client> Clients { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Plan> Plans { get; set; }
    }
}
