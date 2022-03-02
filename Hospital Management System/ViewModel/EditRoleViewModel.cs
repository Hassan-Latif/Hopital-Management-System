using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.ViewModel
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Required(ErrorMessage = "Role Name is required")]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
