using Microsoft.EntityFrameworkCore;
using SalaryApplication.Models;

namespace SalaryApplication.Data
{


    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }


    }
}
