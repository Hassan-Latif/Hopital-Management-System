using AutoMapper;
using Hospital_Management_System.Models;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Services.Interfaces;
using Hospital_Management_System.ViewModel;

namespace Hospital_Management_System.Services
{
    public class RoomServices:IRoomServices
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomServices(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task<List<RoomsViewModel>> GetAllAsync()
        {
            var room = await _roomRepository.GetAll();
            return _mapper.Map<List<RoomsViewModel>>(room);
        }
        public async Task<RoomsViewModel> GetByIdAsync(int id)
        {
            var room= await _roomRepository.GetById(id);
            if (room == null)
            {
                return null;
            }
            return _mapper.Map<RoomsViewModel>(room);

        }
        public async Task RemoveAsync(int id)
        {
            var room=await _roomRepository.GetById(id);
            _roomRepository.Remove(room);
            await _roomRepository.SaveChangingAsync();
        }
        public async Task AddAsync(RoomsViewModel room)
        {
            var Dbroom=_mapper.Map<Rooms>(room);
            _roomRepository.Add(Dbroom);
            await _roomRepository.SaveChangingAsync();
        }
        public async Task UpdateAsync(RoomsViewModel room)
        {
            var Dbroom = _mapper.Map<Rooms>(room);
            _roomRepository.Update(Dbroom);
            await _roomRepository.SaveChangingAsync();
        }




    }
}
