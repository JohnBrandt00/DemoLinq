using DemoLinq.Context;
using DemoLinq.Models;
using System;
using System.Linq;

namespace DemoLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using(var context = new CompanyContext())
            {
                var emp = new Employee { EmployeeId = 199, Name = "Naomi Nagata", Salary = 1000.43 };
                context.Employees.Add(emp);
                context.SaveChanges();

                context.Employees.Select(x => new { Name = x.Name }).ToList().ForEach(x => Console.WriteLine(x.Name));
            }
        }
    }
}
