namespace SalaryApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class User : Account
    {
        public User()
        {

        }
        public User(string firstName, string lastName, string username, string password, string role, int salary, int employeeNumber) 
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
            PassWord = password;
            Role = role;
            Salary = salary;
            EmployeeNumber = employeeNumber;
           
        }
        public int Salary { get; set; }
        public string Role { get; set; }
    }
}
