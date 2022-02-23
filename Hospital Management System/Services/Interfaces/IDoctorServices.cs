using Hospital_Management_System.ViewModel;

namespace Hospital_Management_System.Services.Interfaces
{
    public interface IDoctorServices
    {
        Task<List<DoctorViewModel>> GetAllAsync();
        Task<DoctorViewModel> GetByIdAsync(int id);
        Task AddAsync(DoctorViewModel viewModel);
        Task RemoveAsync(int id);
        Task UpdateAsync(DoctorViewModel viewModel);
    }
}
