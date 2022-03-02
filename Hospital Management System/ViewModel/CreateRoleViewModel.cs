using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
