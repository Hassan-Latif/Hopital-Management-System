using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.ViewModel
{
    public class RoomsViewModel
    {
        public int RoomId { get; set; }
        [Display(Name ="Room Name")]
        public string RoomName { get; set; }
        [Display(Name ="Room Type")]
        public string RoomTypeName { get; set; }
        public int RoomCapacity { get; set; }
        [Display(Name ="Room Type Id")]
        public int RoomTypeId { get; set; }
        public DateTime? RoomDate { get; set; }

    }
}
