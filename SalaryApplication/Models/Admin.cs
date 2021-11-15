namespace SalaryApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Admin: User
    {
        public bool IsAdmin { get; set; }

        public Admin()
        {

        }
    }

   
}
