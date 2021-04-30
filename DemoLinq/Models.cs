using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLinq.Models
{
    public class Employee
    { 
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
    }

    public class Manager 
    {
        public int ManagerId { get; set; }
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }

    }

    public class Department
    { 
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<Manager> Managers { get; set; }
    }


}
