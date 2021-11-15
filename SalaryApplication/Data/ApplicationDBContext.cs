using Microsoft.EntityFrameworkCore;
using SalaryApplication.Models;
using System;
using System.IO;

namespace SalaryApplication.Data
{


    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public ApplicationDBContext()
        {

        }
        private const string DatabaseName = "SalaryApplication";
        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer($@"Server= .\SQLEXPRESS;Database={DatabaseName};trusted_connection=true");
        }
    }
}
