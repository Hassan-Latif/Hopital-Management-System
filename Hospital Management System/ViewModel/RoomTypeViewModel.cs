using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.ViewModel
{
    public class RoomTypeViewModel
    {
        public int RoomTypeId { get; set; }
        [Display(Name ="Room Type")]
        public string RoomTypeName { get; set; }
        public string Description { get; set; }
        [Display(Name ="Created Date")]
        public DateTime? RoomTypeDateTime { get; set; }
    }
}
