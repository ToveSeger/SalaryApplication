namespace SalaryApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class Account
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Employee number")]
        [Range(minimum:10000, maximum:99999)]
        public int EmployeeNumber { get; set; }
        [Display(Name = "First name")]
        [Required(ErrorMessage ="You need to fill in a first name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "You need to fill in a last name")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "You need to fill in a username")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You need to fill in a password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"\S*(\S*([a-zA-Z]\S*[0-9])|([0-9]\S*[a-zA-Z]))\S*")]
        public string PassWord { get; set; }

 
    }
}
