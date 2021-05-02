using DemoLinq.Context;
using DemoLinq.Factories;
using DemoLinq.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoLinq
{
    class Program
    {
   

    

        private static ServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<CompanyFactory>();
            services.AddScoped<Program>();
            services.AddDbContext<CompanyContext>(o => o.UseSqlite(@"Data Source=C:\Users\notjo\Source\Repos\DemoLinq\DemoLinq\work.db"));
            
            return services.BuildServiceProvider(); ;
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Demo'ing the entity framework using linq and dependency injection");
          
            var x = ConfigureServices();

            // get the database service
            CompanyFactory factory = x.GetService<CompanyFactory>();

            #region hide this ugly

            var dep1 = new Department { DepartmentId = 105, DepartmentName = "Engineering"};
            var dep2= new Department { DepartmentId = 106, DepartmentName = "Human Resources" };
            var dep3 = new Department { DepartmentId = 107, DepartmentName = "Programming"};
            var dep4 = new Department { DepartmentId = 108, DepartmentName = "Sales" };

            var man1 = new Manager { Department = dep2, ManagerId = 201, Name = "James Holden" };
            var man2 = new Manager { Department = dep1, ManagerId = 202, Name = "Naomi Nagata" };
            var man3 = new Manager { Department = dep3, ManagerId = 203, Name = "Amos Burton" };
            var man4 = new Manager { Department = dep4, ManagerId = 204, Name = "Alex Kamal" };

            var emp1 = new Employee { Name = "Chrisjen Avasarala", Department = dep1 , EmployeeId = 301 , Salary = 103.5};
            var emp2 = new Employee { Name = "John Smith", Department = dep2, EmployeeId = 302, Salary = 99.9 };
            var emp3 = new Employee { Name = "Matt Smith", Department = dep3, EmployeeId = 303, Salary = 102.7 };
            var emp4 = new Employee { Name = "David Tennant", Department = dep4, EmployeeId = 304, Salary = 92.3 };
            var emp5 = new Employee { Name = "Peter Capaldi", Department = dep1, EmployeeId = 305, Salary = 55.55 };
            var emp6 = new Employee { Name = "Lord Huron", Department = dep1, EmployeeId = 306, Salary = 89.999 };
            var emp7 = new Employee { Name = "Surf Curse", Department = dep2, EmployeeId = 307, Salary = 122312.4 };
            var emp8 = new Employee { Name = "test_employee", Department = dep4, EmployeeId = 308, Salary = 239293.23 };


            //factory.AddDepartment(dep1);
            //factory.AddDepartment(dep2);
            //factory.AddDepartment(dep3);
            //factory.AddDepartment(dep4);

            //factory.AddManager(man1);
            //factory.AddManager(man2);
            //factory.AddManager(man3);
            //factory.AddManager(man4);


            //factory.AddEmployee(emp1);
            //factory.AddEmployee(emp2);
            //factory.AddEmployee(emp3);
            //factory.AddEmployee(emp4);
            //factory.AddEmployee(emp5);
            //factory.AddEmployee(emp6);
            //factory.AddEmployee(emp7);
            //factory.AddEmployee(emp8);

            #endregion


            // PrintTables(factory);

            //AddEmployee(factory);

            // var x1 = GetEmployeeByName(factory);

            //   var x2 =  factory.getDepartmentFromEmployee(x1);
            // Console.WriteLine("{0} {1}", x2.Department.DepartmentName, x2.Employee.Name);

            // GetManager().ForEach(m => Console.WriteLine(m));

            //  Console.WriteLine(GetManager());

            bool quit = true;
            while (quit) 
                {
                Console.WriteLine("1: Print Tables\n 2: Get Employee By Name \n 3: Get Employees of Manager\n 4: Get Employees of department\n 5: Add new employee");

                    switch (Console.ReadKey().Key)
                    {
                    case (ConsoleKey.D1):
                        PrintTables(factory);
                        break;
                    case (ConsoleKey.D2):
                        Console.Clear();
                       Console.WriteLine( GetEmployeeByName(factory));
                        break;

                    case (ConsoleKey.D3):
                        Console.Clear();
                        factory.GetEmployeesOfManager(GetManager());
                        break;
                    case (ConsoleKey.D4):
                        Console.Clear();
                        var depse = factory.GetDepartmentEmployees(GetDepartmentByName());
                        Console.WriteLine(depse.Manager);
                        depse.employees.ForEach(e => Console.WriteLine(e));
                        
                        break;
                    case (ConsoleKey.D5):
                        Console.Clear();
                        AddEmployee(factory);

                        break;


                    default:
                        Console.WriteLine("Not an option");
                        break;
                    };
                };


        }









        public static void PrintTables(CompanyFactory factory)
        {
            Console.WriteLine("\t\t\t Employees: ");
            foreach (var item in factory.getEmployees())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\t\t\t Departments: ");
            foreach (var item in factory.GetDepartments())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\t\t\t Managers: ");
            foreach (var item in factory.GetManagers())
            {
                Console.WriteLine(item);
            }
        }


        public static void AddEmployee(CompanyFactory factory)
        {
            Console.WriteLine("Employee Name:");
                var n = Console.ReadLine();

            Console.WriteLine("Employee ID:");
                var i = int.Parse(Console.ReadLine());

            Console.WriteLine("Employee Salary:");
                var s = double.Parse( Console.ReadLine());

            Console.WriteLine("Employee Department");
            Department d = factory.getDepartmentByID(int.Parse(Console.ReadLine()));

            Employee employee = new Employee { Department = d, EmployeeId = i, Name = n, Salary = s };

            factory.AddEmployee(employee);

        }

        public static Employee GetEmployeeByName(CompanyFactory factory)
        {
            return factory.GetEmployeeByName(Console.ReadLine());
        }

        public static Department GetDepartmentByName()
        {
            var services = ConfigureServices().GetService<CompanyFactory>();
            return services.GetDepartmentByName(Console.ReadLine());
        }

        public static Manager GetManager()
        {
            var services = ConfigureServices().GetService<CompanyFactory>();

            return services.GetManager(Console.ReadLine());
        }
      

    }



 

}

#region boring
//using(var context = new CompanyContext())
//{
//    //var emp = new Employee { EmployeeId = 199, Name = "Naomi Nagata", Salary = 1000.43 };
//    //context.Employees.Add(emp);

//    Console.WriteLine(context.Employees.FirstOrDefault().ToString());

//    var man = new Manager { ManagerId = 100, Name = "James Holden", Employees = new List<Employee>()};
//    var dep = new Department { DepartmentId = 1, DepartmentName = "Water Department", Managers = new List<Manager>() };

//    dep.Managers.Add(context.Managers.FirstOrDefault());
//    man.Employees.Add( context.Employees.FirstOrDefault());

//    //context.Departments.Add(dep);
//    // context.Managers.Add(man);
//    //context.SaveChanges();
//   // context.SaveChanges();
//    context.Employees.Select(x => new { Name = x.Name }).ToList().ForEach(x => Console.WriteLine(x.Name));
//    context.Managers.Select(x => new { Name = x.Name, Emps = x.Employees }).ToList().ForEach(x =>
//    {
//        Console.WriteLine("Manager: {0}", x.Name);
//        x.Emps.ForEach(y => Console.Write("Employee under manager: {0}", y.Name));
//    });

//    var deps = (from d in context.Departments select d);
//    foreach (var item in deps)
//    {
//        Console.WriteLine(item.DepartmentName);

//        item.Managers.ForEach(x => Console.WriteLine("Managed by {0}", x.Name));
//    }

//}


//Console.WriteLine(factory.getDepartmentFromEmployee(factory.getEmployees().FirstOrDefault()).);
// Console.WriteLine(factory.GetManagers().FirstOrDefault()) ;
//foreach (var item in factory.GetEmployeesOfManager(factory.GetManagers().FirstOrDefault()))
//{
//    Console.WriteLine(item);
//}


//foreach (var item in factory.getEmployees() )
//{
//    Console.WriteLine(item.ToString());
//}

//factory.getDepartmentFromEmployee( factory.GetEmployeeByName("Naomi Nagata"));

//factory.UpdateEmployee(199, new Employee { Department = factory.GetDepartments().FirstOrDefault(), EmployeeId = 199, Name = "Naomi Nagata", Salary = 11223.33 });
//factory.GetEmployeesOfManager(factory.GetManagers().FirstOrDefault());

//         foreach (var item in factory.getEmployees())
//{
//    Console.WriteLine(item.ToString());
//}


#endregion
