using DemoLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLinq.Context
{
    class CompanyContext : DbContext
    {
        public DbSet<Employee> Employees {get;set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\notjo\source\repos\FinalProject\DemoLinq\DemoLinq\work.db");
        }

    }
}
