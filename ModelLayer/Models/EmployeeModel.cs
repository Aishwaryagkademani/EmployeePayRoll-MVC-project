using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class EmployeeModel
    {
        [Required(ErrorMessage ="{0} should not be empty" )]
        [StringLength(25,MinimumLength =3, ErrorMessage = "First name start with Cap and Should have minimum 3 character")]
        [RegularExpression(@"^[A-Z][a-zA-Z ]{2,64}$",ErrorMessage ="First name is not valid")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public string Gender { get; set; } 
        [Required]
        public string Department { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public decimal Salary { get; set; }
        [Required]
        public DateTime StartDate { get; set; }= DateTime.Now;
        [Required]
        public string Notes { get; set; }
    }
}
