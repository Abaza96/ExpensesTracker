using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseID { get; set; }
        [DisplayName("Expense")]
        [StringLength(30)]
        [Required(ErrorMessage = "This Field is Required")]
        public string Name { get; set; }
        [DisplayName("Amount")]
        [Required(ErrorMessage = "This Field is Required")]
        public float Cost { get; set; }
        [DisplayName("Created Expense at")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime CreatedAt { set; get; }
        [DisplayName("Modified Expense at")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime UpdatedAt { get; set; }
    }
}
