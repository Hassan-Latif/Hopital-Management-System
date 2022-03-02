using Hospital_Management_System.DbContext;
using Hospital_Management_System.Models;
using Hospital_Management_System.Repositories.Interfaces;

namespace Hospital_Management_System.Repositories
{
    public class RoomRepository: Repository<Rooms>, IRoomRepository
    {
        public readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context):base(context)
        {
            _context=context;
        }
    }
}
