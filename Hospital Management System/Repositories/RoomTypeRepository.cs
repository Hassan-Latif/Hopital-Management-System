using Hospital_Management_System.DbContext;
using Hospital_Management_System.Models;
using Hospital_Management_System.Repositories.Interfaces;

namespace Hospital_Management_System.Repositories
{
    public class RoomTypeRepository: Repository<RoomType>, IRoomTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomTypeRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
