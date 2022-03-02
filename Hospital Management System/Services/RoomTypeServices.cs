using AutoMapper;
using Hospital_Management_System.Models;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Services.Interfaces;
using Hospital_Management_System.ViewModel;

namespace Hospital_Management_System.Services
{
    public class RoomTypeServices:IRoomTypeServices
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;
        public RoomTypeServices(IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<RoomTypeViewModel>> GetAllAsync()
        {
            var roomType = await _roomTypeRepository.GetAll();
            return _mapper.Map<List<RoomTypeViewModel>>(roomType);
        }
        public async Task<RoomTypeViewModel> GetByIdAsync(int id)
        {
            var roomType= await _roomTypeRepository.GetById(id);
            if(roomType == null)
            {
                return null;
            }
            return _mapper.Map<RoomTypeViewModel>(roomType);
        }
        public async Task RemoveAsync(int id)
        {
            var roomType=await _roomTypeRepository.GetById(id);
            _roomTypeRepository.Remove(roomType);
            await _roomTypeRepository.SaveChangingAsync();
            
        }
        public async Task AddAsync(RoomTypeViewModel roomType)
        {
            var DbroomType=_mapper.Map<RoomType>(roomType);
            _roomTypeRepository.Add(DbroomType);
            await _roomTypeRepository.SaveChangingAsync();
        }
        public async Task UpdateAsync(RoomTypeViewModel roomType)
        {
            var DbroomType = _mapper.Map<RoomType>(roomType);
            _roomTypeRepository.Update(DbroomType);
            await _roomTypeRepository.SaveChangingAsync();
        }
    }
}
