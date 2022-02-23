using AutoMapper;
using Hospital_Management_System.Models;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Services.Interfaces;
using Hospital_Management_System.ViewModel;

namespace Hospital_Management_System.Services
{
    public class DoctorService: IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }
        public async Task<List<DoctorViewModel>> GetAllAsync()
        {
            var doctor = await _doctorRepository.GetAll();
            return _mapper.Map<List<DoctorViewModel>>(doctor);
        }

        public async Task<DoctorViewModel> GetByIdAsync(int id)
        {
            var doctor= await _doctorRepository.GetById(id);
            if (doctor == null)
            {
                return null;
            }
            return _mapper.Map<DoctorViewModel>(doctor);

        }
        public async Task RemoveAsync(int id)
        {
            var doctor=await _doctorRepository.GetById(id);
            _doctorRepository.Remove(doctor);
            await _doctorRepository.SaveChangingAsync();
        }
        public async Task AddAsync(DoctorViewModel doctors)
        {
            var dbDoctor = _mapper.Map<Doctor>(doctors);
            _doctorRepository.Add(dbDoctor);
            await _doctorRepository.SaveChangingAsync();
        }
        public async Task UpdateAsync(DoctorViewModel doctors)
        {
            var dbDoctor= _mapper.Map<Doctor>(doctors);
            _doctorRepository.Update(dbDoctor);
            await _doctorRepository.SaveChangingAsync();
        }
    }
}
