namespace SalaryApplication.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SalaryApplication.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDBContext
                (serviceProvider.GetRequiredService<DbContextOptions<ApplicationDBContext>>()))
            {

                if (context.Accounts.Any())
                {
                    return;
                }

                context.Users.Add(
                    new User
                    {
                        FirstName = "Jesper",
                        LastName = "Persson",
                        EmployeeNumber = 123456,
                        UserName = "jeppan",
                        PassWord = "login123",
                        Salary = 10000,
                        Role = "dev",
                    }
                    );

                context.Users.Add(
                new Admin
                {
                    IsAdmin = true,
                    FirstName = "Tove",
                    LastName = "Seger",
                    EmployeeNumber = 123457,
                    UserName = "ToveSeger",
                    PassWord = "login1234",
                    Salary = 40000,
                    Role = "Manager"
                }
                );

                context.SaveChanges();
            }
        }
    }
}


