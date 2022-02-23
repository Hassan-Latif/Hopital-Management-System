using Hospital_Management_System.DbContext;
using Hospital_Management_System.Models;
using Hospital_Management_System.Repositories.Interfaces;

namespace Hospital_Management_System.Repositories
{
    public class DoctorRepository:Repository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
