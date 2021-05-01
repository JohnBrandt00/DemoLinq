using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public override string ToString()
        {
            return new StringBuilder()
                .Append("Employee ID: ")
                .AppendLine(EmployeeId.ToString())
                .Append("Name: ")
                .AppendLine(Name)
                .Append("Salary: ")
                .AppendLine(Salary.ToString())
                .ToString();
        }

    }

    public class Manager 
    {
        public int ManagerId { get; set; }
        public string Name { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Manager ID: ")
                .AppendLine(ManagerId.ToString())
                .Append("Name: ")
                .AppendLine(Name)
                .Append("Department: ")
                .AppendLine(Department.ToString())
                .ToString();
        }

    }

    public class Department
    { 
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Dept ID: ")
                .AppendLine(DepartmentId.ToString())
                .Append("Dept Name: ")
                .AppendLine(DepartmentName)
                .ToString();
        }
    }


}
