using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Models
{
    public class Rooms
    {
        [Key]
        public int RoomId { get; set; }
        [Display(Name ="Room Name")]
        public string RoomName { get; set; }
        [Display(Name ="Room Type")]
        public string RoomTypeName { get; set; }
        [Display(Name ="Room Capacity")]
        public int RoomCapacity { get; set;}
        [Display(Name ="Date")]
        public DateTime? RoomDate { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

    }
}
