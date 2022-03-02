using AutoMapper;
using Hospital_Management_System.Models;
using Hospital_Management_System.ViewModel;

namespace Hospital_Management_System.Config
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorViewModel>().ReverseMap();
            CreateMap<Rooms, RoomsViewModel>().ReverseMap();
            CreateMap<RoomType, RoomTypeViewModel>().ReverseMap();

        }
    }
}
