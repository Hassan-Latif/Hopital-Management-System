using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
    }
}
