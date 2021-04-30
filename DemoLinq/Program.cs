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
                var dep = new Department { DepartmentId = 1, DepartmentName = "Water Department", Managers = new List<Manager>() };

                dep.Managers.Add(context.Managers.FirstOrDefault());
                man.Employees.Add( context.Employees.FirstOrDefault());

                //context.Departments.Add(dep);
                // context.Managers.Add(man);
                //context.SaveChanges();
               // context.SaveChanges();
                context.Employees.Select(x => new { Name = x.Name }).ToList().ForEach(x => Console.WriteLine(x.Name));
                context.Managers.Select(x => new { Name = x.Name, Emps = x.Employees }).ToList().ForEach(x =>
                {
                    Console.WriteLine("Manager: {0}", x.Name);
                    x.Emps.ForEach(y => Console.Write("Employee under manager: {0}", y.Name));
                });

                var deps = (from d in context.Departments select d);
                foreach (var item in deps)
                {
                    Console.WriteLine(item.DepartmentName);

                    item.Managers.ForEach(x => Console.WriteLine("Managed by {0}", x.Name));
                }

            }
        }
    }
}
