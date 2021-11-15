namespace SalaryApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Admin: User
    {
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }

        public Admin()
        {

        }
    }

   
}
