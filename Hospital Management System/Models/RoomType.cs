using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Models
{
    public class RoomType
    {
        [Display(Name ="Id")]
        public int RoomTypeId { get; set; }
        [Required]
        [Display(Name ="Room Type")]
        public string RoomTypeName { get; set; }
        public string Description { get; set; }
        [Display(Name ="Created Date")]
        public DateTime? RoomTypeDateTime { get; set; }
        public List<Rooms> Room { get; set; }

    }
}
