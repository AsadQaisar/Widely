using System.ComponentModel.DataAnnotations;

namespace Widely.Models
{
    public class tbl_Amount
    {
        [Key]
        public int AmountID { get; set; }

        //public string AmountDescription { get; set; }

        public int UserID { get; set; }

        public int Balance { get; set; }

        //public int CreatedBy { get; set; }
        //public string CreatedOn { get; set; }
        //public string ModifyOn { get; set; }
        //public int ModifyBy { get; set; }
        //public int IsActive { get; set; }
    }
}
