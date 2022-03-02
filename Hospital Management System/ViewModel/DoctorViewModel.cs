using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.ViewModel
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Specialization { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
    }
}
