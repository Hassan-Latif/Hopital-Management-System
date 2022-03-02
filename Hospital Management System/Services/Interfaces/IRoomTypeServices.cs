using Hospital_Management_System.ViewModel;

namespace Hospital_Management_System.Services.Interfaces
{
    public interface IRoomTypeServices
    {
        Task<List<RoomTypeViewModel>> GetAllAsync();
        Task<RoomTypeViewModel> GetByIdAsync(int id);
        Task AddAsync(RoomTypeViewModel RoomType);
        Task RemoveAsync(int id);
        Task UpdateAsync(RoomTypeViewModel RoomType);

    }
}
