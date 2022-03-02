using Hospital_Management_System.ViewModel;

namespace Hospital_Management_System.Services.Interfaces
{
    public interface IRoomServices
    {
        Task<List<RoomsViewModel>> GetAllAsync();
        Task<RoomsViewModel> GetByIdAsync(int id);
        Task AddAsync(RoomsViewModel RoomType);
        Task RemoveAsync(int id);
        Task UpdateAsync(RoomsViewModel RoomType);
    }
}
