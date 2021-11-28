using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(30)]
        public string Lender { get; set; }
        [DisplayName("Item")]
        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(30)]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(30)]
        public string Borrower { get; set; }
        [DisplayName("Lended at")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Data Modified at")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
        public DateTime UpdatedAt { get; set; }
    }
}
