using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;

namespace LibraryReservedSystem.Models
{
    public class UserProfile
    {

        public int Id { get; set; }
        [DisplayName("User Name")]
        [Required(ErrorMessage = "This field is required")]
        public string UserName { get; set; } = null!;
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter Email...")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Occupation")]
        public int RoleId { get; set; }
        public string Name { get; set; } = null!;

        // this is the ID I am going to user for right now
        [Display(Name = "WVUID")]
        public int WVUID { get; set; }

        // int that keeps track of how many items a user has checked out. maximum number is 3 items
        [Display(Name = "Number of Items Checked-Out")]
        public int itemCount { get; set; }

        public Role Role { get; set; } = null!;
        public UserProfile()
        {
            
        }
    }
}

