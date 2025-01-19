using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Widely.Models
{
    public class tbl_User
    {
        [Key]
        [Display (Name = "Account ID")]
        public int UserID { get; set; }

        [Display (Name ="First Name")]
        [Required (ErrorMessage ="Required!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required (ErrorMessage = "Required!")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required (ErrorMessage = "Required!")]
        [DataType (DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required (ErrorMessage = "Required!")]
        public string Phone { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required (ErrorMessage = "Required!")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required!")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }

        public int Balance { get; set; }

        //public int CreatedBy { get; set; }
        //public string CreatedOn { get; set; }
        //public string ModifyOn { get; set; }
        //public int ModifyBy { get; set; }
        //public int UserRoleID { get; set; }
        //public int IsActive { get; set; }

    }
}
