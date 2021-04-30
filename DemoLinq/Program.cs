using DemoLinq.Context;
using DemoLinq.Models;
using System;
using System.Collections.Generic;
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
                //var emp = new Employee { EmployeeId = 199, Name = "Naomi Nagata", Salary = 1000.43 };
                //context.Employees.Add(emp);

                Console.WriteLine(context.Employees.FirstOrDefault());

                var man = new Manager { ManagerId = 100, Name = "James Holden", Employees = new List<Employee>()};

                man.Employees.Add( context.Employees.FirstOrDefault());
                context.Managers.Add(man);
                context.SaveChanges();

                context.Employees.Select(x => new { Name = x.Name }).ToList().ForEach(x => Console.WriteLine(x.Name));
                context.Managers.Select(x => new { Name = x.Name, Emps = x.Employees }).ToList().ForEach(x =>
                {
                    Console.WriteLine(x.Name);
                    x.Emps.ForEach(y => Console.Write(y.Name));
                });
            }
        }
    }
}
