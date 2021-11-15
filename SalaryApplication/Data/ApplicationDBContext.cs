using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SalaryApplication.Models;

namespace SalaryApplication.Data
{


    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public string Database = "SalaryApplication.db";
        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var myFolder = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.ToString();
            var path = Path.Combine(myFolder, "Databases");
            Directory.CreateDirectory(path);
            path = Path.Combine(path, Database);
            optionsBuilder.UseSqlite($"Data Source={path}; ");
        }
    }
}
